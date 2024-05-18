using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Toolkit.Foundation;

namespace Bitvault;

public class EditItemHander(IDbContextFactory<ContainerDbContext> dbContextFactory) :
    IHandler<EditEventArgs<(int, ItemConfiguration)>, bool>
{
    public async Task<bool> Handle(EditEventArgs<(int, ItemConfiguration)> args,
        CancellationToken cancellationToken)
    {
        //if (args.Value is ItemConfiguration configuration)
        //{
        //    try
        //    {
        //        using ContainerDbContext context = dbContextFactory.CreateDbContext();
        //        EntityEntry<ItemEntry>? result = null;

        //        await Task.Run(async () =>
        //        {
        //            result = await context.AddAsync(new ItemEntry { Name = configuration.Name }, cancellationToken);
        //            await context.SaveChangesAsync(cancellationToken);

        //        }, cancellationToken);

        //        if (result is not null)
        //        {
        //            return true;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        return false;
    }
}

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

public class ConfirmItemHandler(IMediator mediator,
    IValueStore<Item> valueStore,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        (bool result, int index) result = await mediator.Handle<CreateEventArgs<ItemConfiguration>,
            (bool, int)>(new CreateEventArgs<ItemConfiguration>(new ItemConfiguration()));

        ItemHeaderConfiguration? configuration = await mediator.Handle<ConfirmEventArgs<Item>,
            ItemHeaderConfiguration>(args);
    }
}
