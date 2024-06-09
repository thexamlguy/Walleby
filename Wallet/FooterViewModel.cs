using Toolkit.Foundation;

namespace Wallet;

public partial class FooterViewModel :
    ObservableCollection<IMainNavigationViewModel>
{
    public FooterViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Add<ManageNavigationViewModel>();
    }
}