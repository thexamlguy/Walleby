using Toolkit.Foundation;

namespace Bitvault;

public partial class PasswordViewModel : Observable
{
    public PasswordViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
    }
}