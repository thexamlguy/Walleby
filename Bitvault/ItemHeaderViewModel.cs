using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemHeaderViewModel : Observable<string, string>,
    IHandler<ValidationEventArgs<Item>, bool>,
    IHandler<ConfirmEventArgs<Item>, ItemHeaderConfiguration>,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>,
    IItemEntryViewModel
{
    [ObservableProperty]
    private ItemState state;

    public ItemHeaderViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        ItemState state,
        string? value = null) : base(provider, factory, mediator, publisher, subscriber, disposer, value)
    {
        State = state;
        Value = value;

        Track(nameof(Value), () => Value, newValue => Value = newValue);
    }

    public Task<bool> Handle(ValidationEventArgs<Item> args,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }

    public Task<ItemHeaderConfiguration> Handle(ConfirmEventArgs<Item> args,
        CancellationToken cancellationToken) => Task.FromResult(new ItemHeaderConfiguration { Name = Value! });

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