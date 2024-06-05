using Toolkit.Foundation;

namespace Bitvault;

[Notification(typeof(CreateEventArgs<ItemSectionViewModel>), nameof(ItemContentViewModel))]
public partial class ItemContentViewModel(IServiceProvider provider,
    IServiceFactory factory, IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template,
    ItemState state = ItemState.Read) :
    ObservableCollection<ItemSectionViewModel>(provider, factory, mediator, publisher, subscriber, disposer),
    IItemEntryViewModel,
    INotificationHandler<NotifyEventArgs<ItemCategory<string>>>
{
    public IContentTemplate Template { get; set; } = template;

    public Task Handle(NotifyEventArgs<ItemCategory<string>> args)
    {
        if (args.Value is ItemCategory<string> category)
        {
            if (category.Value is string value)
            {
                Fetch(() => new SynchronizeExpression(new SynchronizeEventArgs<IItemEntryViewModel, 
                    (string, ISynchronizationCollection<ItemSectionViewModel>)>((value, this))), true);
            }
        }

        return Task.CompletedTask;
    }
}
