using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class FilterLockerNavigationViewModel : Observable,
    ILockerNavigationViewModel,
    INotificationHandler<ActivatedEventArgs<Locker>>,
    INotificationHandler<DeactivatedEventArgs<Locker>>
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private string? filter;

    [ObservableProperty]
    private bool selected;

    public FilterLockerNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Filter = filter;
    }

    public Task Handle(DeactivatedEventArgs<Locker> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Locker> args) =>
        Task.FromResult(Activated = true);

    [RelayCommand]
    public void Invoke() => Publisher.Publish(Notify.As(new Filter(Filter)),
        nameof(ItemCollectionViewModel));
}