using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class EditItemHander(IDbContextFactory<ContainerDbContext> dbContextFactory) :
    IHandler<EditEventArgs<(int, ItemConfiguration)>, (bool, int, string?)>
{
    public async Task<(bool, int, string?)> Handle(EditEventArgs<(int, ItemConfiguration)> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is (int id, ItemConfiguration configuration))
        {
            try
            {
                using ContainerDbContext context = dbContextFactory.CreateDbContext();
                ItemEntry? result = null;

                await Task.Run(async () =>
                {
                    result = await context.Set<ItemEntry>().FindAsync(id);

                    if (result is not null)
                    {
                        result.Name = configuration.Name;
                        await context.SaveChangesAsync(cancellationToken);
                    }    

                }, cancellationToken);

                if (result is not null)
                {
                    return (true, result.Id, result.Name);
                }
            }
            catch
            {

            }
        }

        return (false, -1, "");
    }
}
