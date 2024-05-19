using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateItemHander(IDbContextFactory<ContainerDbContext> dbContextFactory) : 
    IHandler<CreateEventArgs<ItemConfiguration>, (bool, int)>
{
    public async Task<(bool, int)> Handle(CreateEventArgs<ItemConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is ItemConfiguration configuration)
        {
            try
            {
                using ContainerDbContext context = dbContextFactory.CreateDbContext();
                EntityEntry<ItemEntry>? result = null;

                await Task.Run(async () =>
                {
                    result = await context.AddAsync(new ItemEntry { Name = configuration.Name }, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);

                }, cancellationToken);

                if (result is not null)
                {
                    return (false, -1);
                }
            }
            catch
            {

            }
        }

        return (false, -1);
    }
}
