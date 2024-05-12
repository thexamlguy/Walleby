using Toolkit.Foundation;

namespace Bitvault;

public partial class ArchiveNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string name) : FilterContainerNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, name);