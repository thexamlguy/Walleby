using Toolkit.Foundation;

namespace Bitvault;

public partial class DateEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ItemState state,
    ItemEntryConfiguration configuration,
    string key,
    object value,
    double width) : ItemEntryViewModel(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, width);
