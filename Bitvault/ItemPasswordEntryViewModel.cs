using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemPasswordEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ItemState state,
    ItemEntryConfiguration configuration,
    string key,
    object value) : ItemEntryViewModel(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value);