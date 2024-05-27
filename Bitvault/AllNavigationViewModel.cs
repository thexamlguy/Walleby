﻿using Toolkit.Foundation;

namespace Bitvault;

public partial class AllNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string filter) : FilterLockerNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, filter);