using Toolkit.Foundation;

namespace Bitvault;

public class VaultContentViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer)
{
    public IContentTemplate Template { get; set; } = template;
}