using Toolkit.Foundation;

namespace Wallet;

public partial class CategoryNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string value) : FilterNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, 0, value);