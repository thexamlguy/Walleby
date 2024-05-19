using Toolkit.Foundation;

namespace Bitvault;

public partial class ContainerHeaderViewModel : 
    ObservableCollection
{
    public ContainerHeaderViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;

        Add<CreateItemActionViewModel>(0);
        Add<SearchContainerActionViewModel>(2);
    }

    public IContentTemplate Template { get; set; }
}
