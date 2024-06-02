using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemPasswordEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string type,
    string? key = default,
    object? value = default) : ItemEntryViewModel<string, object?>(provider, factory, mediator, publisher, subscriber, disposer, type, key, value),
    IItemEntryViewModel;