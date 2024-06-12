﻿using Toolkit.Foundation;

namespace Wallet;

public partial class PasswordEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    ItemState state,
    ItemEntryConfiguration configuration,
    string key,
    string value,
    double width) : ItemEntryViewModel<string>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, width);