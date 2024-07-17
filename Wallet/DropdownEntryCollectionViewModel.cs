using Toolkit.Foundation;

namespace Wallet;

public partial class DropdownEntryCollectionViewModel :
    ItemEntryCollectionViewModel<DropdownEntryViewModel, object>
{
    public DropdownEntryCollectionViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IEnumerable<DropdownEntryViewModel> items,
        ItemState state,
        DropdownEntryCollectionConfiguration configuration,
        string key,
        object value,
        bool isConcealed,
        bool isRevealed,
        double width,
        DropdownEntryViewModel selectedItem) : base(provider, factory, mediator, publisher, subscriber, disposer, items, state, configuration, key, value, isConcealed, isRevealed, width)
    {
        SelectedItem = selectedItem;
    }

    public DropdownEntryCollectionViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IEnumerable<DropdownEntryViewModel> items,
        ItemState state,
        DropdownEntryCollectionConfiguration configuration,
        string key,
        object value,
        bool isConcealed,
        bool isRevealed,
        double width) : base(provider, factory, mediator, publisher, subscriber, disposer, items, state, configuration, key, value, isConcealed, isRevealed, width)
    {
    }
}