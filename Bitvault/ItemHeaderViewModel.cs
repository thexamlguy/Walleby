using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemHeaderViewModel : Observable<string, string>,
    IHandler<ValidationEventArgs<Item>, bool>,
    IHandler<ConfirmEventArgs<Item>, ItemHeaderConfiguration>,
    INotificationHandler<EditEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>,
    IItemEntryViewModel
{
    [ObservableProperty]
    private bool immutable;

    public ItemHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    bool immutable,
    string? value = null) : base(provider, factory, mediator, publisher, subscriber, disposer, value)
    {
        Immutable = immutable;
        Value = value;

        Track(nameof(Value), () => Value, newValue => Value = newValue);
    }

    public Task<bool> Handle(ValidationEventArgs<Item> args,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }

    public Task<ItemHeaderConfiguration> Handle(ConfirmEventArgs<Item> args,
        CancellationToken cancellationToken) => Task.FromResult(new ItemHeaderConfiguration { Name = Value });

    public Task Handle(EditEventArgs<Item> args) =>
        Task.FromResult(Immutable = false);

    public Task Handle(CancelEventArgs<Item> args)
    {
        Revert();
        Immutable = true;

        return Task.CompletedTask;
    }

    public Task Handle(ConfirmEventArgs<Item> args)
    {
        Commit();
        Immutable = true;

        return Task.CompletedTask;
    }
}