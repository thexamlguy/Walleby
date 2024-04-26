using Toolkit.Foundation;

namespace Bitvault;

public partial class ManageViewModel :
    ObservableCollectionViewModel,
    IMainNavigationViewModel
{
    public ManageViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;

        Add<CreateVaultNavigationViewModel>();
    }

    public IContentTemplate Template { get; set; }
}