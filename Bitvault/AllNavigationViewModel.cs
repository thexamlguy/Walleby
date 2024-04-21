﻿using Toolkit.Foundation;

namespace Bitvault;

public class AllNavigationViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) :
        ObservableViewModel(serviceProvider, serviceFactory, publisher, subscriber, disposer), 
        IMainNavigationViewModel;