using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemDropdownEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ItemEntryConfiguration configuration,
    string? key = default,
    object? value = default) : ItemEntryViewModel(provider, factory, mediator, publisher, subscriber, disposer, configuration, key, value);