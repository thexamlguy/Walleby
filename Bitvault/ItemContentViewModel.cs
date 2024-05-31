using Toolkit.Foundation;

namespace Bitvault;

//[Aggerate(nameof(ItemContentViewModel))]
public partial class ItemContentViewModel :
    ObservableCollection<IItemEntryViewModel>,
    IItemEntryViewModel,
    INotificationHandler<NotifyEventArgs<ItemCategory<string>>>
{
    public ItemContentViewModel(IServiceProvider provider,
        IServiceFactory factory, IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        ItemState state = ItemState.Read) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
    }

    public IContentTemplate Template { get; set; }

    protected override IAggerate OnAggerate(object? key)
    {
        return base.OnAggerate(key);
    }


    public Task Handle(NotifyEventArgs<ItemCategory<string>> args)
    {
        return Task.CompletedTask;
    }
}
