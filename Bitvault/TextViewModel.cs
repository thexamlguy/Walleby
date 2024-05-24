using Toolkit.Foundation;

namespace Bitvault;

public partial class TextViewModel : Observable
{
    public TextViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
    }
}