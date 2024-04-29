using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class LockViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private string? password;

    [RelayCommand]
    private void Unlock()
    {
        if (Password is { Length: > 0 })
        {
            Mediator.Handle<Open<Vault>, bool>(Open.As(new Vault(Password)));
        }
    }
}