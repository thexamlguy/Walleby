﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class AddVaultContentActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    NamedComponent named) : ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer)
{

    [ObservableProperty]
    private string named = $"{named}";
}