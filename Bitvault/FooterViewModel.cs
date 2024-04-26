using Toolkit.Foundation;

namespace Bitvault;

public partial class FooterViewModel :
    ObservableCollectionViewModel<IMainNavigationViewModel>
{
    public FooterViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Add<ManageNavigationViewModel>();
    }
}
