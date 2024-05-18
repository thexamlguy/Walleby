using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class FilterContainerNavigationViewModel : Observable,
    IContainerNavigationViewModel,
    INotificationHandler<ActivatedEventArgs<ContainerToken>>,
    INotificationHandler<DeactivatedEventArgs<ContainerToken>>
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private string? filter;

    [ObservableProperty]
    private bool selected;

    public FilterContainerNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Filter = filter;
    }

    public Task Handle(DeactivatedEventArgs<ContainerToken> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<ContainerToken> args) =>
        Task.FromResult(Activated = true);

    [RelayCommand]
    public void Invoke() => Publisher.Publish(Notify.As(new Filter(Filter)), 
        nameof(ContainerViewModel));
}