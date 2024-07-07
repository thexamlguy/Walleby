using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;
using Wallet.Data;

namespace Wallet;

public class DeleteItemHandler(IDbContextFactory<WalletContext> dbContextFactory) :
    IHandler<DeleteEventArgs<Item<Guid>>, bool>
{
    public async Task<bool> Handle(DeleteEventArgs<Item<Guid>> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is Item<Guid> item)
        {
            Guid id = item.Value;

            using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            if (await context.FindAsync<ItemEntity>(id) is ItemEntity result)
            {
                context.Items.Remove(result);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
        return false;
    }
}