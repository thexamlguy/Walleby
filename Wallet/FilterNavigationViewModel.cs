using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class FilterNavigationViewModel : 
    ObservableCollection<IWalletNavigationViewModel, int, string>,
    IWalletNavigationViewModel,
    INotificationHandler<ActivatedEventArgs<Wallet>>,
    INotificationHandler<DeactivatedEventArgs<Wallet>>
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private bool selected;

    public FilterNavigationViewModel(IServiceProvider provider, 
        IServiceFactory factory, 
        IMediator mediator, 
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        int key, 
        string value) : base(provider, factory, mediator, publisher, subscriber, disposer, key, value)
    {

    }

    public Task Handle(DeactivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = true);
}

public partial class FilterNavigationViewModel<TWalletNavigation> :
    ObservableCollection<TWalletNavigation, int, string>,
    IWalletNavigationViewModel,
    INotificationHandler<ActivatedEventArgs<Wallet>>,
    INotificationHandler<DeactivatedEventArgs<Wallet>>
    where TWalletNavigation :
    IWalletNavigationViewModel
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private bool selected;

    public FilterNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator, 
        IPublisher publisher,
        ISubscription subscriber, 
        IDisposer disposer,
        int key,
        string value) : base(provider, factory, mediator, publisher, subscriber, disposer, key, value)
    {

    }

    public Task Handle(DeactivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = true);
}