using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemDropdownEntryViewModel : 
    ItemEntryCollectionViewModel<ItemDropdownValueViewModel>
{
    public ItemDropdownEntryViewModel(IServiceProvider provider,
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer, 
        IEnumerable<ItemDropdownValueViewModel> items,
        ItemState state, 
        ItemEntryConfiguration configuration, 
        string key,
        object value,
        ItemDropdownValueViewModel selectedItem) : base(provider, factory, mediator, publisher, subscriber, disposer, items, state, configuration, key, value)
    {
        SelectedItem = selectedItem;
    }

    public ItemDropdownEntryViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IEnumerable<ItemDropdownValueViewModel> items,
        ItemState state,
        ItemEntryConfiguration configuration,
        string key,
        object value) : base(provider, factory, mediator, publisher, subscriber, disposer, items, state, configuration, key, value)
    {
    }
}