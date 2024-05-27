using Toolkit.Foundation;

namespace Bitvault;

public partial class StarredNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string name) : FilterLockerNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, name);