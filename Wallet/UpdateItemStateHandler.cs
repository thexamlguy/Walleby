using Wallet.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Wallet;

public class UpdateItemStateHandler(IDbContextFactory<WalletContext> dbContextFactory) : 
    IHandler<UpdateEventArgs<(Guid, int)>, bool>
{
    public async Task<bool> Handle(UpdateEventArgs<(Guid, int)> args, 
        CancellationToken cancellationToken)
    {
        if (args.Sender is (Guid id, int state))
        {
            using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            if (await context.FindAsync<ItemEntity>(id) is ItemEntity result)
            {
                result.State = state;
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        return false;
    }
}
