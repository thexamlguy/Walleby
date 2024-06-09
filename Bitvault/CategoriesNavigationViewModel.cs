using Toolkit.Foundation;

namespace Bitvault;

public partial class CategoriesNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string name) : FilterWalletNavigationViewModel(provider, factory, mediator, publisher, subscriber, disposer, name);