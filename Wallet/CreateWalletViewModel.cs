using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolkit.Foundation;

namespace Wallet;

public partial class CreateWalletViewModel : Observable,
    IPrimaryConfirmation
{
    [MaybeNull]
    [ObservableProperty]
    private string name;

    [MaybeNull]
    [ObservableProperty]
    private string password;

    [MaybeNull]
    [ObservableProperty]
    private string? repeatedPassword;

    [ObservableProperty]
    private IValidation validation;

    public CreateWalletViewModel(IValidation validation, 
        IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator, 
        IPublisher publisher,
        ISubscriber subscriber, 
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Validation = validation;

        Validation.Add(() => Name, [new ValidationRule(() => Name is { Length: > 0 })],
            ValidationTrigger.Immediate);

        Validation.Add(() => Password, [new ValidationRule(() => Password is { Length: > 0 })], 
            ValidationTrigger.Immediate);

        Name = name;
    }

    public async Task<bool> ConfirmPrimary()
    {
        using (await new ActivityLock(this))
        {
            return await Mediator.Handle<CreateEventArgs<Wallet<(string, string)>>,
                bool>(Create.As(new Wallet<(string, string)>((Name, Password))));
        }
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs args)
    {
        if (args.PropertyName is string name)
        {
            Validation.Validate(name);
        }

        base.OnPropertyChanged(args);
    }
}