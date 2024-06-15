using Toolkit.Foundation;

namespace Wallet;

public class ArchiveItemHandler(IDecoratorService<Item<(Guid, string)>> decoratorService,
    ICache<Item<(Guid, string)>> cache,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<ArchiveEventArgs<Item>>
{
    public async Task Handle(ArchiveEventArgs<Item> args)
    {
        try
        {
            if (decoratorService.Service is Item<(Guid, string)> item)
            {
                if (cache.Contains(item))
                {
                    (Guid id, string name) = item.Value;

                    await mediator.Handle<UpdateEventArgs<(Guid, int)>,
                        bool>(new UpdateEventArgs<(Guid, int)>((id, 2)));

                    cache.Remove(item);
                    publisher.Publish(Changed.As<Item>());
                }
            } 
        }
        catch
        {

        }
    }
}