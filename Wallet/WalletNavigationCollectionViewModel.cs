using Toolkit.Foundation;

namespace Wallet;

[Notification(typeof(CreateEventArgs<IWalletNavigationViewModel>), nameof(WalletNavigationCollectionViewModel))]
[Notification(typeof(InsertEventArgs<IWalletNavigationViewModel>), nameof(WalletNavigationCollectionViewModel))]
public partial class WalletNavigationCollectionViewModel :
    ObservableCollection<IWalletNavigationViewModel>,
    INotificationHandler<SelectionEventArgs<IMainNavigationViewModel>>
{
    public WalletNavigationCollectionViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber, 
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(SelectionEventArgs<IMainNavigationViewModel> args)
    {
        if (args.Sender is not null)
        {
            SelectedItem = null;
        }

        return Task.CompletedTask;
    }
}
