using Toolkit.Foundation;

namespace Wallet;

public partial class CategoryNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string filter) : FilterNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, filter);