using Toolkit.Foundation;

namespace Bitvault;

public partial class FooterViewModel :
    ObservableCollectionViewModel<IMainNavigationViewModel>
{
    public FooterViewModel(IServiceProvider serviceProvider, 
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        Add<ManageNavigationViewModel>();
    }
}
