﻿using Toolkit.Foundation;

namespace Wallet;

public partial class DateEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    ItemState state,
    IItemEntryConfiguration<DateTimeOffset?> configuration,
    string key,
    DateTimeOffset? value,
    bool isConcealed,
    bool isRevealed,
    double width) : ItemEntryViewModel<DateTimeOffset?>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, isConcealed, isRevealed, width);