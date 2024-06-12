using Toolkit.Foundation;

namespace Wallet;

public partial class ItemCommandHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template) :
    ObservableCollection(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<NotifyEventArgs<ItemCommandHeaderCollection>>
{
    public IContentTemplate Template { get; set; } = template;

    public Task Handle(NotifyEventArgs<ItemCommandHeaderCollection> args)
    {
        if (args.Sender is ItemCommandHeaderCollection commandCollection)
        {
            Clear(args =>
            {
                foreach (IDisposable command in commandCollection)
                {
                    args.Add(command);
                }
            });
        }
     
        return Task.CompletedTask;
    }
}