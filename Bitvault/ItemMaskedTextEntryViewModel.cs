using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemMaskedTextEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string? key = default,
    object? value = default) : Observable<string, object?>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    IItemEntryViewModel;
