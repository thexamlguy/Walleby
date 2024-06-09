using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class UpdateItemStateHandler(IDbContextFactory<WalletContext> dbContextFactory) : 
    IHandler<UpdateEventArgs<(Guid, int)>, bool>
{
    public async Task<bool> Handle(UpdateEventArgs<(Guid, int)> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is (Guid id, int state))
        {
            await Task.Run(async () =>
            {
                using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
                if (await context.FindAsync<ItemEntry>(id) is ItemEntry result)
                {
                    result.State = state;
                    await context.SaveChangesAsync();
                }
            }, cancellationToken);
        }

        return false;
    }
}
