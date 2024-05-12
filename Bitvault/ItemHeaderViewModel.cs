using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) : ObservableViewModel<string, string>(provider, factory, mediator, publisher, subscriber, disposer),
    IItemViewModel
{
    public void Invoke(ItemConfiguration args) => 
        args.Name = Value;
}
