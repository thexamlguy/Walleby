using Toolkit.Foundation;

namespace Bitvault;

public partial class PasswordViewModel : ObservableViewModel
{
    public PasswordViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {

    }
}
