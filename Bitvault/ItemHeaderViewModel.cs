using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemHeaderViewModel : 
    Observable<string, string>,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>,
    IHandler<ValidationEventArgs<ItemHeader>, bool>,
    IHandler<ConfirmEventArgs<ItemHeader>, string?>,
    INotificationHandler<NotifyEventArgs<ItemCategory<string>>>
{
    [ObservableProperty]
    private string? category;

    [ObservableProperty]
    private ItemState state;

    public ItemHeaderViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        ItemState state,
        string key,
        string value) : base(provider, factory, mediator, publisher, subscriber, disposer, key, value)
    {
        State = state;
        Value = value;

        Track(nameof(Value), () => Value, newValue => Value = newValue);
    }

    public Task<bool> Handle(ValidationEventArgs<ItemHeader> args,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
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

    public Task Handle(NotifyEventArgs<ItemCategory<string>> args)
    {
        if (args.Value is ItemCategory<string> category)
        {
            Category = category.Value;
        }

        return Task.CompletedTask;
    }

    public Task<string> Handle(ConfirmEventArgs<ItemHeader> args,
        CancellationToken cancellationToken) => Task.FromResult(Value);
}