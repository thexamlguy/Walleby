using Toolkit.Foundation;

namespace Bitvault;

public partial class VaultNavigationViewModel :
    ObservableCollectionViewModel<IMainNavigationViewModel>,
    IMainNavigationViewModel
{
    public VaultNavigationViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        Template = template;

        Add<AllNavigationViewModel>();
        Add<StarredNavigationViewModel>();
        Add<ArchiveNavigationViewModel>();
        Add<CategoriesNavigationViewModel>();
    }

    public IContentTemplate Template { get; set; }
}