using Toolkit.Foundation;

namespace Bitvault;

public partial class ManageNavigationViewModel : 
    ObservableViewModel, 
    IMainNavigationViewModel
{
    public ManageNavigationViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher, 
        ISubscriber subscriber, 
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {

    }
}
