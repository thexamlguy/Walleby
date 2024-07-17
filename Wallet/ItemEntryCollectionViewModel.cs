using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemEntryCollectionViewModel<TItem, TValue> :
    ObservableCollection<TItem, string, TValue>,
    IItemEntryViewModel,
    IHandler<ValidateEventArgs<ItemEntry>, bool>,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>
    where TItem : notnull,
    IDisposable
{
    private readonly IItemEntryConfiguration<TValue> configuration;

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
        IItemEntryConfiguration<TValue> configuration,
        string key,
        TValue value,
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
        IItemEntryConfiguration<TValue> configuration,
        string key,
        TValue value,
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

    public Task Handle(UpdateEventArgs<Item> args)
    {
        State = ItemState.Write;
        OnStateChanged();

        return Task.CompletedTask;
    }

    public Task Handle(CancelEventArgs<Item> args)
    {
        Revert();

        State = ItemState.Read;
        OnStateChanged();

        return Task.CompletedTask;
    }

    protected virtual void OnStateChanged()
    {
    }

    public Task Handle(ConfirmEventArgs<Item> args)
    {
        Commit();

        State = ItemState.Read;
        OnStateChanged();

        return Task.CompletedTask;
    }

    public async Task<bool> Handle(ValidateEventArgs<ItemEntry> args,
        CancellationToken cancellationToken)
    {
        if (configuration is not null)
        {
            configuration.Value = Value;
        }

        return await Task.FromResult(true);
    }

    [RelayCommand]
    private void Copy() => Publisher.Publish(Write.As(new Clipboard<object>($"{Value}")));

    [RelayCommand]
    private void Hide() => IsRevealed = false;

    [RelayCommand]
    private void Reveal() => IsRevealed = true;
}