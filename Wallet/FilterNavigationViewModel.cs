using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class FilterNavigationViewModel : 
    ObservableCollection<IWalletNavigationViewModel>,
    IWalletNavigationViewModel,
    INotificationHandler<ActivatedEventArgs<Wallet>>,
    INotificationHandler<DeactivatedEventArgs<Wallet>>
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private string? filter;

    [ObservableProperty]
    private bool selected;

    public FilterNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Filter = filter;
    }

    public Task Handle(DeactivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = true);
}

public partial class FilterNavigationViewModel<TWalletNavigation> :
    ObservableCollection<TWalletNavigation>,
    IWalletNavigationViewModel,
    INotificationHandler<ActivatedEventArgs<Wallet>>,
    INotificationHandler<DeactivatedEventArgs<Wallet>>
    where TWalletNavigation :
    IWalletNavigationViewModel
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private string? filter;

    [ObservableProperty]
    private bool selected;

    public FilterNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Filter = filter;
    }

    public Task Handle(DeactivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = true);
}