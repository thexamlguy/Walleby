﻿using Toolkit.Foundation;

namespace Bitvault;

public class AllNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) :
        ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer),
        IMainNavigationViewModel;