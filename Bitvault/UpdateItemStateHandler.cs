using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class UpdateItemStateHandler(IDbContextFactory<ContainerDbContext> dbContextFactory) : 
    IHandler<UpdateEventArgs<(Guid, int)>, bool>
{
    public async Task<bool> Handle(UpdateEventArgs<(Guid, int)> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is (Guid id, int state))
        {
            await Task.Run(async () =>
            {
                using ContainerDbContext context = await dbContextFactory.CreateDbContextAsync();
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
