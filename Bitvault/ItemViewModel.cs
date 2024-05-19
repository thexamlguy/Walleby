using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemViewModel : 
    ObservableCollection<IDisposable>
{
    public ItemViewModel(IServiceProvider provider, 
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher, 
        ISubscription subscriber, 
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;

        Publisher.Publish(Notify.As(Factory.Create<CommandCollection>(new List<IDisposable>
        {
            Factory.Create<EditItemActionViewModel>(),
            Factory.Create<ArchiveItemActionViewModel>(),
        })));
    }

    public IContentTemplate Template { get; set; }
}
