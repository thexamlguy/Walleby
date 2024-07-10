using Toolkit.Foundation;

namespace Wallet;

public partial class FooterViewModel :
    ObservableCollection<INavigationViewModel>
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