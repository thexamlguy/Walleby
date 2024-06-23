using Wallet.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;
using Toolkit.Foundation;

namespace Wallet;

public class UpdateItemHander(IDbContextFactory<WalletContext> dbContextFactory) :
    IHandler<UpdateEventArgs<Item<(Guid, string, ItemConfiguration)>>, bool>
{
    public async Task<bool> Handle(UpdateEventArgs<Item<(Guid, string, ItemConfiguration)>> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is Item<(Guid, string, ItemConfiguration)> item)
        {
            (Guid id, string name, ItemConfiguration configuration) = item.Value;

            try
            {
                using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
                ItemEntry? result = result = await context.Set<ItemEntry>().FindAsync([id], cancellationToken);

                if (result is not null)
                {
                    string content = JsonSerializer.Serialize(configuration);
                    result.Blobs.Add(new()
                    {
                        Data = Encoding.UTF8.GetBytes(content),
                        DateTime = DateTime.Now,
                        Type = 0,
                    });

                    result.Name = name;
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