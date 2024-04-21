using Toolkit.Foundation;

namespace Bitvault;

[Notification(nameof(MainViewModel))]
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
    }

    public IContentTemplate Template { get; set; }
}
