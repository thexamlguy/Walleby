using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemEntryCollectionViewModel<TItem> : 
    ObservableCollection<TItem, string, object>,
    IItemEntryViewModel,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>
    where TItem : notnull, 
    IDisposable
{
    private readonly ItemEntryConfiguration configuration;

    [ObservableProperty]
    private bool isConcealed;

    [ObservableProperty]
    private bool isRevealed;

    [ObservableProperty]
    private ItemState state;
    [ObservableProperty]
    private double width;

    public ItemEntryCollectionViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        ItemState state,
        ItemEntryConfiguration configuration,
        string key,
        object value,
        bool isConcealed,
        bool isRevealed,
        double width) : base(provider, factory, mediator, publisher, subscriber, disposer, key, value)
    {
        this.configuration = configuration;

        State = state;
        IsConcealed = isConcealed;
        IsRevealed = isRevealed;
        Width = width;

        Track(nameof(Value), () => Value, x => Value = x);
    }

    public ItemEntryCollectionViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IEnumerable<TItem> items,
        ItemState state,
        ItemEntryConfiguration configuration,
        string key,
        object value,
        bool isConcealed,
        bool isRevealed,
        double width) : base(provider, factory, mediator, publisher, subscriber, disposer, items, key, value)
    {
        this.configuration = configuration;

        State = state;
        IsConcealed = isConcealed;
        IsRevealed = isRevealed;
        Width = width;

        Track(nameof(Value), () => Value, x => Value = x);
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

    protected override void OnValueChanged()
    {
        if (configuration is not null)
        {
            configuration.Value = Value;
        }
    }

    [RelayCommand]
    private void Copy() => Publisher.Publish(Write.As(new Clipboard<object>($"{Value}")));

    [RelayCommand]
    private void Hide() => IsRevealed = false;

    [RelayCommand]
    private void Reveal() => IsRevealed = true;
}