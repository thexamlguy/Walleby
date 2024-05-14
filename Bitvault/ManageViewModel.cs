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
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;

        Add<CreateContainerNavigationViewModel>();
    }

    public IContentTemplate Template { get; set; }
}