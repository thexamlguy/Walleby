﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolkit.Foundation;

namespace Bitvault;

public partial class CreateVaultViewModel(IServiceProvider provider,
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

    public async Task<bool> Confirm()
    {
        return await Mediator.Handle<Create<Vault>, bool>(Create.As(new Vault(Name)));
    }
}