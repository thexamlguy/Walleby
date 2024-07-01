using Wallet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;
using System.Text.Json;
using Toolkit.Foundation;

namespace Wallet;

public class CreateItemHandler(IImageWriter imageWriter,
    IDbContextFactory<WalletContext> dbContextFactory) :
    IHandler<CreateEventArgs<(Guid, string, string, IImageDescriptor?, ItemConfiguration)>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<(Guid, string, string, IImageDescriptor?, ItemConfiguration)> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is (Guid id, string name, string category, IImageDescriptor imageDescriptor, ItemConfiguration configuration))
        {
            try
            {
                string content = JsonSerializer.Serialize(configuration);
                byte[]? thumbData = null;

                if (imageDescriptor is not null)
                {
                    using MemoryStream memoryStream = new MemoryStream();
                    imageWriter.Write(imageDescriptor, memoryStream);
                    thumbData = memoryStream.ToArray();
                }

                ItemEntry itemEntry = new()
                {
                    Id = id,
                    Name = name,
                    Category = category,
                    Image = thumbData != null ? new BlobEntry
                    {
                        Id = Guid.NewGuid(),
                        Data = thumbData,
                        DateTime = DateTime.UtcNow,
                        Type = 1
                    } : null,
                    Blobs =
                    { 
                        new BlobEntry
                        {
                            Id = Guid.NewGuid(),
                            Data = Encoding.UTF8.GetBytes(content),
                            DateTime = DateTime.UtcNow,
                            Type = 0
                        }
                    }
                };

                using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
                EntityEntry<ItemEntry>? result = await context.AddAsync(itemEntry, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

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