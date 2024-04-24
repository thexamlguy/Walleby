using Toolkit.Foundation;

namespace Bitvault;

public partial class CreateVaultNavigationViewModel :
    ObservableViewModel,
    IMainNavigationViewModel
{
    public CreateVaultNavigationViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {

    }
}
