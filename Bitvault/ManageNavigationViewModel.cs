using Toolkit.Foundation;

namespace Bitvault;

public partial class ManageNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer),
    IMainNavigationViewModel;

public partial class VaultCommandViewModel : ObservableCollectionViewModel
{
    public VaultCommandViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator, 
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Add<SearchVaultCommandViewModel>();
        Add<CreateLockerCommandViewModel>();
    }
}

public partial class SearchVaultCommandViewModel :
    ObservableCollectionViewModel
{
    public SearchVaultCommandViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
    }
}

public partial class CreateLockerCommandViewModel : 
    ObservableCollectionViewModel
{
    public CreateLockerCommandViewModel(IServiceProvider provider, 
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
    }
}