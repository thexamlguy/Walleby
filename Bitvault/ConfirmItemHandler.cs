using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmItemHandler(IMediator mediator,
    IDbContextFactory<ContainerDbContext> dbContextFactory,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task<bool> Handle(ConfirmEventArgs<Item> args,
        CancellationToken cancellationToken)
    {
        await mediator.Handle<ConfirmEventArgs<Item>, bool>(args);
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
        //            Item item = new() { Id = result.Entity.Id, Name = configuration.Name };
        //            publisher.Publish(Activated.As(item), cancellationToken);

        //            return true;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        return false;
    }

    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        ItemHeaderConfiguration? headerConfiguration = await mediator.Handle<ConfirmEventArgs<Item>, ItemHeaderConfiguration>(args);
    }
}
