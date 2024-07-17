using Toolkit.Foundation;

namespace Wallet;

[Notification(typeof(CreateEventArgs<ItemSectionViewModel>), nameof(ItemContentViewModel))]
public partial class ItemContentViewModel(IServiceProvider provider,
    IServiceFactory factory, IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template) :
    ObservableCollection<ItemSectionViewModel>(provider, factory, mediator, publisher, subscriber, disposer),
    IItemEntryViewModel,
    INotificationHandler<NotifyEventArgs<ItemCategory<string>>>,
    IItemViewModel
{
    public IContentTemplate Template { get; set; } = template;

    public Task Handle(NotifyEventArgs<ItemCategory<string>> args)
    {
        if (args.Sender is ItemCategory<string> category
            && category.Value is string value)
        {
            Activate(() => new ActivationBuilder(new ActivationEventArgs<IItemEntryViewModel,
                string>(value)), true);
        }

        return Task.CompletedTask;
    }
}