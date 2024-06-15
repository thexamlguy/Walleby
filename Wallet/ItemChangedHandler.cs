using Toolkit.Foundation;

namespace Wallet;

public class ItemChangedHandler(IMediator mediator,
    IPublisher publisher) : 
    INotificationHandler<ChangedEventArgs<Item>>
{
    public async Task Handle(ChangedEventArgs<Item> args)
    {
        IReadOnlyCollection<(string, int)>? counts = await mediator.Handle<CountEventArgs<Item>, 
            IReadOnlyCollection<(string, int)>>(Count.As<Item>());

        if (counts is { Count: > 0 } ) 
        {
            foreach ((string key, int count) in counts)
            {
                publisher.Publish(Notify.As(new Item<int>(count)), key);
            }
        }
    }
}
