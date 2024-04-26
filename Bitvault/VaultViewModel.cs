using Toolkit.Foundation;

namespace Bitvault;

public class VaultViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer);
