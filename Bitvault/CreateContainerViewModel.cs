﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolkit.Foundation;

namespace Bitvault;

public partial class CreateContainerViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IPublisher publisher,
    IMediator mediator,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer),
    IPrimaryConfirmation
{
    [MaybeNull]
    [ObservableProperty]
    private string name;

    [MaybeNull]
    [ObservableProperty]
    private string password;

    public async Task<bool> Confirm() => 
        await Mediator.Handle<Create<Container>, bool>(Create.As(new Container(Name, Password)));
}