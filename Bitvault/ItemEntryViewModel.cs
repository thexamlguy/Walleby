using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemEntryViewModel<TKey, TValue>(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ICollectionSynchronization<IItemEntryViewModel> synchronization,
    ItemEntryConfiguration configuration,
    TKey? key = default,
    TValue? value = default) :
    Observable<TKey, TValue>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    IHandler<ConfirmEventArgs<ItemContentEntry>, ItemEntryConfiguration>,
    IItemEntryViewModel,
    IIndexable
{
    public int Index => synchronization.IndexOf(this);

    public Task<ItemEntryConfiguration> Handle(ConfirmEventArgs<ItemContentEntry> args,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(configuration with { Value = Value });
    }
}
