using Toolkit.Foundation;

namespace Bitvault;

public partial class DropdownEntryViewModel : 
    ItemEntryCollectionViewModel<DropdownValueViewModel>
{
    public DropdownEntryViewModel(IServiceProvider provider,
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer, 
        IEnumerable<DropdownValueViewModel> items,
        ItemState state, 
        ItemEntryConfiguration configuration, 
        string key,
        object value,
        DropdownValueViewModel selectedItem) : base(provider, factory, mediator, publisher, subscriber, disposer, items, state, configuration, key, value)
    {
        SelectedItem = selectedItem;
    }

    public DropdownEntryViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IEnumerable<DropdownValueViewModel> items,
        ItemState state,
        ItemEntryConfiguration configuration,
        string key,
        object value) : base(provider, factory, mediator, publisher, subscriber, disposer, items, state, configuration, key, value)
    {
    }
}