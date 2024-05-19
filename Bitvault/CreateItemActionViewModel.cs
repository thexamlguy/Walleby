﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class CreateItemActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    NamedComponent named,
    int index) : Observable(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private int index = index;

    [ObservableProperty]
    private string named = $"{named}";
}