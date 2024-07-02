using Wallet.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Toolkit.Foundation;

namespace Wallet;

public class ItemHandler(IDbContextFactory<WalletContext> dbContextFactory) : 
    IHandler<RequestEventArgs<Item<Guid>>, (Guid, string, string?, string, ItemConfiguration?)>
{
    public async Task<(Guid, string, string?, string, ItemConfiguration?)> Handle(RequestEventArgs<Item<Guid>> args, 
        CancellationToken cancellationToken)
    {
        if (args.Sender is Item<Guid> item)
        {
            Guid id = item.Value;

            using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            var result = await context.Set<ItemEntry>()
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    HasImage = x.ImageId != null,
                    x.Description,
                    x.Category,
                    Blob = x.Blobs
                        .Where(b => b.Type == 0)
                        .OrderByDescending(b => b.DateTime)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (result is not null)
            {
                ItemConfiguration? configuration = null;
                if (result.Blob is BlobEntry blob && blob.Data is { Length: > 0 } data)
                {
                    try
                    {
                        configuration = JsonSerializer.Deserialize<ItemConfiguration>(data);
                    }
                    catch
                    {

                    }
                }

                return (result.Id, result.Name, result.Description, result.Category, configuration);
            }
        }

        return default;
    }
}
