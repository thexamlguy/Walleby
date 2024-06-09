using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;
using System.Text.Json;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateItemHandler(IDbContextFactory<WalletContext> dbContextFactory) :
    IHandler<CreateEventArgs<(Guid, string, string, ItemConfiguration)>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<(Guid, string, string, ItemConfiguration)> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is (Guid id, string name, string category, ItemConfiguration configuration))
        {
            try
            {
                string content = JsonSerializer.Serialize(configuration);
                ItemEntry itemEntry = new()
                {
                    Id = id,
                    Name = name,
                    Category = category
                };

                itemEntry.Blobs.Add(new()
                {
                    Data = Encoding.UTF8.GetBytes(content),
                    DateTime = DateTime.Now,
                    Type = 0,
                });

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