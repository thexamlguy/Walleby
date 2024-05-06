using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class FilterVaultNavigationViewModel : ObservableViewModel,
    IVaultNavigationViewModel,
    INotificationHandler<Vault<Activated>>,
    INotificationHandler<Vault<Deactivated>>
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private string? filter;

    [ObservableProperty]
    private bool selected;

    public FilterVaultNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Filter = filter;
    }

    public Task Handle(Vault<Deactivated> args,
        CancellationToken cancellationToken = default) =>
            Task.FromResult(Activated = false);

    public Task Handle(Vault<Activated> args,
        CancellationToken cancellationToken = default) =>
            Task.FromResult(Activated = true);

    [RelayCommand]
    public async Task Invoke() => await Publisher.Publish(Vault.As(new Filter<string>(Filter)));
}