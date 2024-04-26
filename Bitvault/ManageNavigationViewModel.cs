using Toolkit.Foundation;

namespace Bitvault;

public partial class ManageNavigationViewModel : 
    ObservableViewModel, 
    IMainNavigationViewModel
{
    public ManageNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher, 
        ISubscriber subscriber, 
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {

    }
}
