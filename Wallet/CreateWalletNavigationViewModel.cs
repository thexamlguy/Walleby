using Toolkit.Foundation;

namespace Wallet;

public partial class CreateWalletNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    Observable(provider, factory, mediator, publisher, subscriber, disposer),
    INavigationViewModel;