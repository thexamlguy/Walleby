using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolkit.Foundation;

namespace Wallet;

public partial class CreateWalletViewModel :
    Observable,
    IPrimaryConfirmation
{
    [ObservableProperty]
    private IImageDescriptor? imageDescriptor;

    [ObservableProperty]
    private bool isConfirmed;

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
            IsConfirmed = await Mediator.Handle<CreateEventArgs<Wallet<(string, string, IImageDescriptor?)>>,
                bool>(Create.As(new Wallet<(string, string, IImageDescriptor?)>((Name, Password, ImageDescriptor))));

            return IsConfirmed;
        }
    }

    [RelayCommand]
    public async Task Import() => ImageDescriptor = await Mediator.Handle<CreateEventArgs<ProfileImage>,
        IImageDescriptor>(Create.As<ProfileImage>());

    [RelayCommand]
    public void Remove() => ImageDescriptor = null;

    protected override void OnPropertyChanged(PropertyChangedEventArgs args)
    {
        if (args.PropertyName is string name)
        {
            _ = Validation.Validate(name);
        }

        base.OnPropertyChanged(args);
    }
}