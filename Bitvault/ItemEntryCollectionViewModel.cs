using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemEntryCollectionViewModel<TItem> : 
    ObservableCollection<TItem, string, object>,
    IItemEntryViewModel,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>
    where TItem : notnull, 
    IDisposable
{
    [ObservableProperty]
    private ItemState state;

    private readonly ItemEntryConfiguration configuration;

    public ItemEntryCollectionViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        ItemState state,
        ItemEntryConfiguration configuration,
        string key,
        object value,
        double width) : base(provider, factory, mediator, publisher, subscriber, disposer, key, value)
    {
        this.configuration = configuration;

        State = state;
        Width = width;
    }

    [ObservableProperty]
    private double width;


    public ItemEntryCollectionViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IEnumerable<TItem> items,
        ItemState state,
        ItemEntryConfiguration configuration,
        string key,
        object value,
        double width) : base(provider, factory, mediator, publisher, subscriber, disposer, items, key, value)
    {
        this.configuration = configuration;

        State = state;
        Width = width;
    }

    protected override void OnValueChanged()
    {
        if (configuration is not null)
        {
            configuration.Value = Value;
        }
    }

    public Task Handle(UpdateEventArgs<Item> args) =>
        Task.FromResult(State = ItemState.Write);

    public Task Handle(CancelEventArgs<Item> args)
    {
        Revert();

        State = ItemState.Read;
        return Task.CompletedTask;
    }

    public Task Handle(ConfirmEventArgs<Item> args)
    {
        Commit();

        State = ItemState.Read;
        return Task.CompletedTask;
    }
}