using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemEntryViewModel<TValue>(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IClipboardWriter clipboardWriter,
    ItemState state,
    ItemEntryConfiguration configuration,
    string key,
    TValue value,
    bool isConcealed,
    bool isRevealed,
    double width) :
    Observable<string, TValue>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    IItemEntryViewModel,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>
    where TValue : notnull
{
    [ObservableProperty]
    private bool isConcealed = isConcealed;

    [ObservableProperty]
    private bool isRevealed = isRevealed;

    [ObservableProperty]
    private ItemState state = state;

    [ObservableProperty]
    private double width = width;

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

    protected override void OnValueChanged() =>
        configuration.Value = Value;

    [RelayCommand]
    private void Hide() => IsRevealed = false;

    [RelayCommand]
    private void Reveal() => IsRevealed = true;

    [RelayCommand]
    private void Copy() => clipboardWriter.Write($"{Value}");
}