using Toolkit.Foundation;

namespace Wallet;

public partial class ArchiveNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string name) : FilterWalletNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, name);