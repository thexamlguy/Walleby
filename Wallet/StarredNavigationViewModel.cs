using Toolkit.Foundation;

namespace Wallet;

[Notification(typeof(NotifyEventArgs<Item<int>>), "Favourites")]
public partial class StarredNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    int key,
    string value) : FilterNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    INotificationHandler<NotifyEventArgs<Item<int>>>
{
    public Task Handle(NotifyEventArgs<Item<int>> args)
    {
        if (args.Sender is Item<int> item)
        {
            Key = item.Value;
        }

        return Task.CompletedTask;
    }
}