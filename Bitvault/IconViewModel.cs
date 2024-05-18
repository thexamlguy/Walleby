﻿using Toolkit.Foundation;

namespace Bitvault;

public partial class IconViewModel : Observable
{
    public IconViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {

    }
}
