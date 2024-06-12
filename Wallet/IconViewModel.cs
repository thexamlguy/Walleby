using Toolkit.Foundation;

namespace Wallet;

public partial class IconViewModel : Observable
{
    public IconViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
    }
}