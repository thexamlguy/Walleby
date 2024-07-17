using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics.CodeAnalysis;
using Toolkit.Foundation;

namespace Wallet;

public partial class OpenWalletViewModel :
    Observable
{
    [ObservableProperty]
    private IValidation validation;

    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private IImageDescriptor? imageDescriptor;

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
        string name,
        IImageDescriptor? imageDescriptor = default) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        this.validation = validation;

        Name = name;
        ImageDescriptor = imageDescriptor;
    }

    [RelayCommand]
    private async Task Invoke()
    {
        using (await new ActivityLock(this))
        {
            if (await Validation.Validate(() => Password, [new ValidationRule(async () =>
                await Mediator.Handle<OpenEventArgs<Wallet<string>>, bool>(Open.As(new Wallet<string>(Password))),
                    "The password is incorrect, please try again.")]))
            {
                Publisher.Publish(Opened.As<Wallet>());
            }
        }
    }

    public override async Task OnActivated()
    {
        Publisher.Publish(Activated.As<Wallet>());
        await base.OnActivated();
    }

    public override async Task OnDeactivated()
    {
        Publisher.Publish(Deactivated.As<Wallet>());
        await base.OnDeactivated();
    }
}