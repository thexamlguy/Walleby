using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemEntryViewModel<TKey, TValue> :
    Observable<TKey, TValue>
{
    public ItemEntryViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator, 
        IPublisher publisher, 
        ISubscription subscriber,
        IDisposer disposer,
        string type,
        TKey? key = default,
        TValue? value = default) : base(provider, factory, mediator, publisher, subscriber, disposer, key, value)
    {
        Type = type;
    }

    [ObservableProperty]
    private string type;
}
