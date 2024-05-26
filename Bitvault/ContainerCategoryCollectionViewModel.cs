using Toolkit.Foundation;

namespace Bitvault;

[Aggerate(nameof(ContainerCategoryCollectionViewModel))]
public partial class ContainerCategoryCollectionViewModel :
    ObservableCollection<ItemNavigationViewModel>
{
    public ContainerCategoryCollectionViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber, 
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {

    }
}
