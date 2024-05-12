using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class FilterContainerNavigationViewModel : ObservableViewModel,
    IContainerNavigationViewModel,
    INotificationHandler<Container<Activated>>,
    INotificationHandler<Container<Deactivated>>
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
        ISubscriber subscriber,
        IDisposer disposer,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Filter = filter;
    }

    public Task Handle(Container<Deactivated> args,
        CancellationToken cancellationToken = default) =>
            Task.FromResult(Activated = false);

    public Task Handle(Container<Activated> args,
        CancellationToken cancellationToken = default) =>
            Task.FromResult(Activated = true);

    [RelayCommand]
    public async Task Invoke() => await Publisher.Publish(Container.As(new Filter<string>(Filter)));
}