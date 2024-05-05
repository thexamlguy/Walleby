using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class FilterVaultNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string name) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer),
    IVaultNavigationViewModel,
    INotificationHandler<Vault<Activated>>,
    INotificationHandler<Vault<Deactivated>>
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private bool selected;

    public Task Handle(Vault<Deactivated> args,
        CancellationToken cancellationToken = default) =>
            Task.FromResult(Activated = false);

    public Task Handle(Vault<Activated> args,
        CancellationToken cancellationToken = default) =>
            Task.FromResult(Activated = true);

    [RelayCommand]
    public void Invoke() => Publisher.Publish(Vault.As(new Selected<string>(name)));
}