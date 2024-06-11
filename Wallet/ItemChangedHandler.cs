using Toolkit.Foundation;

namespace Wallet;

public class ItemChangedHandler(IMediator mediator,
    IPublisher publisher) : 
    INotificationHandler<ChangedEventArgs<Item<(Guid, string)>>>
{
    public async Task Handle(ChangedEventArgs<Item<(Guid, string)>> args)
    {
        IReadOnlyCollection<(string, int)>? categoryCounts = await mediator.Handle<CountEventArgs<ItemCategory>, 
            IReadOnlyCollection<(string, int)>>(Count.As<ItemCategory>());

        if (categoryCounts is { Count: > 0  } ) 
        {
            foreach ((string key, int count) in categoryCounts)
            {
                publisher.Publish(Notify.As(new Item<int>(count)), key);
            }
        }
    }
}
