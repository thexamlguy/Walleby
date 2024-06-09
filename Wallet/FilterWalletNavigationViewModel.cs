using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class FilterWalletNavigationViewModel : Observable,
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

    public FilterWalletNavigationViewModel(IServiceProvider provider,
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

    [RelayCommand]
    public void Invoke() => Publisher.Publish(Notify.As(new Filter(Filter)),
        nameof(ItemCollectionViewModel));
}