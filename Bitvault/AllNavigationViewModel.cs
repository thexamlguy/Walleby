﻿using Toolkit.Foundation;

namespace Bitvault;

public partial class AllNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string name) : FilterVaultNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, name);
