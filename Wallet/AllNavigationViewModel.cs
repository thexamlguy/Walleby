using Toolkit.Foundation;

namespace Wallet;

public partial class AllNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string filter) : FilterWalletNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, filter);