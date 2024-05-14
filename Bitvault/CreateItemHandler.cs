using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Toolkit.Foundation;

namespace Bitvault;

public class ArchiveItemHandler(IValueStore<Item> valueStore,
    IDbContextFactory<ContainerDbContext> dbContextFactory) :
    INotificationHandler<ArchiveEventArgs<Item>>
{
    public async Task Handle(ArchiveEventArgs<Item> args)
    {
        try
        {
            if (valueStore.Value is Item item)
            {
                await Task.Run(async () =>
                {
                    using ContainerDbContext context = await dbContextFactory.CreateDbContextAsync();

                    if (await context.FindAsync<ItemEntry>(item.Id) is ItemEntry result)
                    {
                        result.State = 3;
                        await context.SaveChangesAsync();
                    }
                });
            }
        }
        catch
        {

        }
    }
}


public class CreateItemHandler(IDbContextFactory<ContainerDbContext> dbContextFactory,
    IPublisher publisher) :
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
                    Item item = new() { Id = result.Entity.Id, Name = configuration.Name };
                    publisher.Publish(Activated.As(item), cancellationToken);

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
