using Toolkit.Foundation;

namespace Wallet;

public class UnarchiveItemHandler(IDecoratorService<Item<(Guid, string)>> decoratorService,
    ICache<Item<(Guid, string)>> cache,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<UnarchiveEventArgs<Item>>
{
    public async Task Handle(UnarchiveEventArgs<Item> args)
    {
        try
        {
            if (decoratorService.Service is Item<(Guid, string)> item)
            {
                (Guid id, string name) = item.Value;

                await mediator.Handle<UpdateEventArgs<(Guid, int)>,
                    bool>(new UpdateEventArgs<(Guid, int)>((id, 0)));

                cache.Add(item);
                publisher.Publish(Changed.As<Item>());
            }
        }
        catch
        {
        }
    }
}