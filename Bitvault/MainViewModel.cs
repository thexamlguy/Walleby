using Toolkit.Foundation;

namespace Bitvault;

public partial class MainViewModel : 
    ObservableCollectionViewModel<IMainNavigationViewModel>
{
    public MainViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        Template = template;
        Add<VaultNavigationViewModel>();
    }

    public IContentTemplate Template { get; set; }
}
