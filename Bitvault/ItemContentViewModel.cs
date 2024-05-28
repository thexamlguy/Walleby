using Toolkit.Foundation;

namespace Bitvault;

[Aggerate(nameof(ItemContentViewModel))]
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

    public Task Handle(NotifyEventArgs<ItemCategory<string>> args)
    {
        throw new NotImplementedException();
    }
}
