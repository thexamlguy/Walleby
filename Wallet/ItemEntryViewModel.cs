using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemEntryViewModel<TValue> :
    Observable<string, TValue>,
    IItemEntryViewModel,
    IRemovable,
    IHandler<ValidateEventArgs<ItemEntry>, bool>,
    INotificationHandler<ConfirmEventArgs<ItemEntry>>,
    INotificationHandler<UpdateEventArgs<ItemEntry>>,
    INotificationHandler<CancelEventArgs<ItemEntry>>
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

    public ItemEntryViewModel(IServiceProvider provider,
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

    public Task Handle(UpdateEventArgs<ItemEntry> args) =>
        Task.FromResult(State = ItemState.Write);

    public Task Handle(CancelEventArgs<ItemEntry> args)
    {
        Revert();

        State = ItemState.Read;
        return Task.CompletedTask;
    }

    public Task Handle(ConfirmEventArgs<ItemEntry> args)
    {
        Commit();

        State = ItemState.Read;
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
    private void Remove() => Dispose();

    [RelayCommand]
    private void Copy() => Publisher.Publish(Write.As(new Clipboard<object>($"{Value}")));

    [RelayCommand]
    private void Hide() => IsRevealed = false;

    [RelayCommand]
    private void Reveal() => IsRevealed = true;
}