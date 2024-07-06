using Toolkit.Foundation;

namespace Wallet;

public partial class DropdownEntryViewModel : 
    ItemEntryCollectionViewModel<DropdownValueViewModel>
{
    public DropdownEntryViewModel(IServiceProvider provider,
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer, 
        IEnumerable<DropdownValueViewModel> items,
        ItemState state, 
        ItemEntryConfiguration configuration, 
        string key,
        object value,
        bool isConcealed,
        bool isRevealed,
        double width,
        DropdownValueViewModel selectedItem) : base(provider, factory, mediator, publisher, subscriber, disposer, items, state, configuration, key, value, isConcealed, isRevealed, width)
    {
        SelectedItem = selectedItem;
    }

    public DropdownEntryViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IEnumerable<DropdownValueViewModel> items,
        ItemState state,
        ItemEntryConfiguration configuration,
        string key,
        object value,
        bool isConcealed,
        bool isRevealed,
        double width) : base(provider, factory, mediator, publisher, subscriber, disposer, items, state, configuration, key, value, isConcealed, isRevealed, width)
    {
    }
}