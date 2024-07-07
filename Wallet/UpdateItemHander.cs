using Wallet.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;
using Toolkit.Foundation;

namespace Wallet;

public class UpdateItemHander(IDbContextFactory<WalletContext> dbContextFactory,
    IImageWriter imageWriter) :
    IHandler<UpdateEventArgs<Item<(Guid, string, string, IImageDescriptor?, ItemConfiguration)>>, bool>
{
    public async Task<bool> Handle(UpdateEventArgs<Item<(Guid, string, string, IImageDescriptor?, ItemConfiguration)>> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is Item<(Guid, string, string, IImageDescriptor?, ItemConfiguration)> item)
        {
            (Guid id, string name, string category, IImageDescriptor? imageDescriptor, ItemConfiguration configuration) = item.Value;

            try
            {
                using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
                ItemEntity? result = result = await context.Set<ItemEntity>()
                    .Include(x => x.Image).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (result is not null)
                {
                    string content = JsonSerializer.Serialize(configuration);
                    result.Blobs.Add(new()
                    {
                        Data = Encoding.UTF8.GetBytes(content),
                        DateTime = DateTime.Now,
                        Type = 0,
                    });

                    if (imageDescriptor is not null)
                    {
                        byte[]? thumbData = null;
                        using MemoryStream memoryStream = new();
                        imageWriter.Write(imageDescriptor, memoryStream);
                        thumbData = memoryStream.ToArray();

                        if (result.Image is BlobEntity existingImageBlob)
                        {
                            existingImageBlob.Data = thumbData;
                            existingImageBlob.DateTime = DateTime.UtcNow;

                            context.Entry(result.Image).State = EntityState.Modified;
                        }
                        else
                        {
                            result.Image = new BlobEntity
                            {
                                Id = Guid.NewGuid(),
                                Data = thumbData,
                                DateTime = DateTime.UtcNow,
                                Type = 1
                            };

                            context.Entry(result.Image).State = EntityState.Added;
                        }
                    }
                    else
                    {
                        if (result.Image is not null)
                        {
                            context.Remove(result.Image);
                            result.Image = null;
                        }
                    }

                    result.Name = name;
                    result.Category = category;

                    await context.SaveChangesAsync(cancellationToken);
                }

                if (result is not null)
                {
                    return true;
                }
            }
            catch
            {
            }
        }

        return false;
    }
}