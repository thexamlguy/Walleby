﻿using Toolkit.Foundation;

namespace Wallet;

public partial class DateEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IClipboardWriter clipboardWriter,
    ItemState state,
    ItemEntryConfiguration configuration,
    string key,
    DateTimeOffset value,
    bool isConcealed,
    bool isRevealed,
    double width) : ItemEntryViewModel<DateTimeOffset>(provider, factory, mediator, publisher, subscriber, disposer, clipboardWriter, state, configuration, key, value, isConcealed, isRevealed, width);
