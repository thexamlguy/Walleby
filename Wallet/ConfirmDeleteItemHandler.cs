using Toolkit.Foundation;

namespace Wallet;

public class ConfirmDeleteItemHandler(IDecoratorService<Item<(Guid, string)>> decoratorService,
    ICache<Item<(Guid, string)>> cache,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<DeleteEventArgs<Item>>
{
    public async Task Handle(DeleteEventArgs<Item> args)
    {
        try
        {
            if (decoratorService.Service is Item<(Guid, string)> item)
            {
                (Guid id, string name) = item.Value;

                await mediator.Handle<DeleteEventArgs<Item<Guid>>,
                    bool>(new DeleteEventArgs<Item<Guid>>(new Item<Guid>(id)));

                cache.Add(item);
                publisher.Publish(Changed.As<Item>());
            }
        }
        catch
        {
        }
    }
}