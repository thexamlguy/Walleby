using Wallet.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Wallet;

public class ItemImageHandler(IDbContextFactory<WalletContext> dbContextFactory,
    IImageReader imageReader) :
    IHandler<RequestEventArgs<ItemImage<Guid>>, IImageDescriptor?>
{
    public async Task<IImageDescriptor?> Handle(RequestEventArgs<ItemImage<Guid>> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is ItemImage<Guid> item)
        {
            Guid id = item.Value;

            using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            var result = await context.Set<ItemEntity>()
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Image
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (result is not null && 
                result.Image is BlobEntity image &&
                image.Data is { Length: > 0 } data)
            {
                MemoryStream stream = new(data);
                return imageReader.Get(stream, 200, 200, true);
            }
        }

        return default;
    }
}
