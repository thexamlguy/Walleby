using Toolkit.Foundation;

namespace Wallet;

[Notification(typeof(CreateEventArgs<IItemEntryViewModel>), nameof(Id))]
public partial class ItemSectionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template,
    string id) : ObservableCollection<IItemEntryViewModel>(provider, factory, mediator, publisher, subscriber, disposer),
    IKeyed<string>
{
    public string Id => id;

    public IContentTemplate Template { get; set; } = template;
}
