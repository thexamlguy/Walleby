using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemDropdownEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ISynchronizationCollection<IItemEntryViewModel> synchronization,
    ItemEntryConfiguration configuration,
    string? key = default,
    string? value = default) : ItemEntryViewModel<string, string?>(provider, factory, mediator, publisher, subscriber, disposer, synchronization, configuration, key, value);