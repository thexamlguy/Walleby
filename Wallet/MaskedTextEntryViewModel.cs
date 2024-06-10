﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

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
    string value,
    double width) : ItemEntryViewModel<string>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, width)
{
    [ObservableProperty]
    private string pattern = pattern;
}