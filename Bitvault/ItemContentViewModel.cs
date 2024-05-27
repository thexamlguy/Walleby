using Toolkit.Foundation;

namespace Bitvault;

[Aggerate(nameof(ItemContentViewModel))]
public partial class ItemContentViewModel :
    ObservableCollection<IItemEntryViewModel>,

    IItemEntryViewModel
{
    public ItemContentViewModel(IServiceProvider provider,
        IServiceFactory factory, IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        bool immutable = true) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
    }

    public IContentTemplate Template { get; set; }
}
