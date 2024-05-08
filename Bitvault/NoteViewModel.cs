using Toolkit.Foundation;

namespace Bitvault;

public partial class NoteViewModel : ObservableViewModel
{
    public NoteViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {

    }
}
