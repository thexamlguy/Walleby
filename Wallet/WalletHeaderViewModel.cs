using Toolkit.Foundation;

namespace Wallet;

public partial class WalletHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template) :
    ObservableCollection(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<NotifyEventArgs<WalletCommandHeaderCollection>>
{
    public IContentTemplate Template { get; set; } = template;

    public Task Handle(NotifyEventArgs<WalletCommandHeaderCollection> args)
    {
        Clear();

        if (args.Sender is WalletCommandHeaderCollection commandCollection)
        {
            foreach (IDisposable command in commandCollection)
            {
                Add(command);
            }
        }

        return Task.CompletedTask;
    }
}