using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string? value = null) : ObservableViewModel<string, string>(provider, factory, mediator, publisher, subscriber, disposer, value),
    IItemViewModel
{
    public void Invoke(ItemConfiguration args) => 
        args.Name = Value;

}
