﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    private IImageDescriptor? image;

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
            IsConfirmed = await Mediator.Handle<CreateEventArgs<Wallet<(string, string)>>,
                bool>(Create.As(new Wallet<(string, string)>((Name, Password))));

            return IsConfirmed;
        }
    }

    [RelayCommand]
    public async Task Import() => Image = await Mediator.Handle<RequestEventArgs<ProfileImage>,
        IImageDescriptor>(Request.As<ProfileImage>());

    protected override void OnPropertyChanged(PropertyChangedEventArgs args)
    {
        if (args.PropertyName is string name)
        {
            _ = Validation.Validate(name);
        }

        base.OnPropertyChanged(args);
    }
}