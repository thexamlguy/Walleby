using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class OpenLockerViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string name) :
    Observable(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private string? name = name;

    [ObservableProperty]
    private string? password;

    [RelayCommand]
    private async Task Invoke()
    {
        if (Password is { Length: > 0 })
        {
            if (await Mediator.Handle<ActivateEventArgs<Locker>, bool>(Activate.As(new Locker(Password))))
            {
                Publisher.Publish(Opened.As<Locker>());
            }
        }
    }
}