using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class OpenWalletViewModel : Observable
{
    private readonly IValidation validation;

    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private string? password;

    [ObservableProperty]
    private string? repeatedPassword;

    public OpenWalletViewModel(IValidation validation,
        IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator, 
        IPublisher publisher, 
        ISubscriber subscriber,
        IDisposer disposer,
        string name) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        this.validation = validation;
        Name = name;
    }

    [RelayCommand]
    private async Task Invoke()
    {
        using (await new ActivityLock(this))
        {
            if (Password is { Length: > 0 })
            {
                if (await Mediator.Handle<ActivateEventArgs<Wallet<string>>, bool>(Activate.As(new Wallet<string>(Password))))
                {
                    Publisher.Publish(Opened.As<Wallet>());
                }
            }
        }
    }
}