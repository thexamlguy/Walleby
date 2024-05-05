using Toolkit.Foundation;

namespace Bitvault;

public partial class AllNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string filter) : FilterVaultNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, filter);
