using Toolkit.Foundation;

namespace Bitvault;

[Notification(nameof(VaultViewModel))]
public partial class VaultViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator, 
    IPublisher publisher, 
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template) : ObservableCollectionViewModel<LockerNavigationViewModel>(provider, factory, mediator, publisher, subscriber, disposer)
{
    public IContentTemplate Template { get; set; } = template;
}