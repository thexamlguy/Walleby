using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics.CodeAnalysis;
using Toolkit.Foundation;

namespace Wallet;

public partial class OpenWalletViewModel : Observable
{
    [ObservableProperty]
    private IValidation validation;

    [ObservableProperty]
    private string? name;

    [MaybeNull]
    [ObservableProperty]
    private string password;

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
            if (await Validation.Validate(() => Password, [new ValidationRule(async () => 
                await Mediator.Handle<ActivateEventArgs<Wallet<string>>, bool>(Activate.As(new Wallet<string>(Password))), 
                    "The password is incorrect, please try again.")]))
            {
                Publisher.Publish(Opened.As<Wallet>());
            }
        }
    }
}