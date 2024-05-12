using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateItemHandler(IDbContextFactory<ContainerDbContext> dbContextFactory, IPublisher publisher) :
    IHandler<CreateEventArgs<ItemConfiguration>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<ItemConfiguration> args,
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
                    await publisher.Publish(Activated.As(new Item { Id = result.Entity.Id }), cancellationToken);
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
