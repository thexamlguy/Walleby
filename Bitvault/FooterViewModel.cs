using Toolkit.Foundation;

namespace Bitvault;

public partial class FooterViewModel :
    ObservableCollection<IMainNavigationViewModel>
{
    public FooterViewModel(ICollectionSynchronizer synchronizer,
        IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer) : base(synchronizer, provider, factory, mediator, publisher, subscriber, disposer)
    {
        Add<ManageNavigationViewModel>();
    }
}