using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class EditItemHander(IDbContextFactory<ContainerDbContext> dbContextFactory) :
    IHandler<EditEventArgs<(Guid, ItemConfiguration)>, bool>
{
    public async Task<bool> Handle(EditEventArgs<(Guid, ItemConfiguration)> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is (Guid id, ItemConfiguration configuration))
        {
            try
            {
                string? name = configuration.Name;

                using ContainerDbContext context = dbContextFactory.CreateDbContext();
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