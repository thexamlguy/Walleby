using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateItemHandler(IDbContextFactory<ContainerDbContext> dbContextFactory) :
    IHandler<CreateEventArgs<(Guid, ItemConfiguration)>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<(Guid, ItemConfiguration)> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is (Guid id, ItemConfiguration configuration))
        {
            try
            {
                string? name = configuration.Name;

                using ContainerDbContext context = dbContextFactory.CreateDbContext();
                EntityEntry<ItemEntry>? result = null;

                await Task.Run(async () =>
                {
                    result = await context.AddAsync(new ItemEntry { Id = id, Name = name }, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
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