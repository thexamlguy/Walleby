using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemEntryViewModel<TKey, TValue>(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ISynchronizationCollection<IItemEntryViewModel> synchronization,
    ItemEntryConfiguration configuration,
    TKey? key = default,
    TValue? value = default) :
    Observable<TKey, TValue>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    IHandler<ConfirmEventArgs<ItemContentEntry>, (int, ItemEntryConfiguration)>,
    IItemEntryViewModel,
    IIndexable
{
    public int Index => synchronization.IndexOf(this);

    public Task<(int, ItemEntryConfiguration)> Handle(ConfirmEventArgs<ItemContentEntry> args,
        CancellationToken cancellationToken) => Task.FromResult((Index, configuration with { Value = Value }));
}
