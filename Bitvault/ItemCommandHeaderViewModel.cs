using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemCommandHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template) :
    ObservableCollection(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<NotifyEventArgs<ItemCommandHeaderCollection>>
{
    public IContentTemplate Template { get; set; } = template;

    public Task Handle(NotifyEventArgs<ItemCommandHeaderCollection> args)
    {
        Clear();

        if (args.Value is ItemCommandHeaderCollection commandCollection)
        {
            foreach (IDisposable command in commandCollection)
            {
                Add(command);
            }
        }

        return Task.CompletedTask;
    }
}