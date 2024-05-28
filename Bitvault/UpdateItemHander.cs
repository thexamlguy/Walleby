using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class UpdateItemHander(IDbContextFactory<LockerContext> dbContextFactory) :
    IHandler<UpdateEventArgs<(Guid, string, ItemConfiguration)>, bool>
{
    public async Task<bool> Handle(UpdateEventArgs<(Guid, string, ItemConfiguration)> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is (Guid id, string name, ItemConfiguration configuration))
        {
            try
            {
                using LockerContext context = dbContextFactory.CreateDbContext();
                ItemEntry? result = null;

                await Task.Run(async () =>
                {
                    result = await context.Set<ItemEntry>().FindAsync(id);
                    if (result is not null)
                    {
                        result.Name = name;
                        await context.SaveChangesAsync(cancellationToken);
                    }
                }, cancellationToken);

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