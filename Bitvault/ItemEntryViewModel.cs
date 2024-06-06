using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ItemEntryConfiguration configuration,
    string? key = default,
    object? value = default) :
    Observable<string, object>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    IItemEntryViewModel,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>
{
    [ObservableProperty]
    private ItemState state = ItemState.Read;

    protected override void OnValueChanged() => 
        configuration.Value = Value;

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
