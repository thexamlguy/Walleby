﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class MaskedTextEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ItemState state,
    ItemEntryConfiguration configuration,
    string pattern,
    string key,
    object value,
    double width) : ItemEntryViewModel(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, width)
{
    [ObservableProperty]
    private string pattern = pattern;
}