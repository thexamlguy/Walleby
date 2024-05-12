using Toolkit.Foundation;

namespace Bitvault;

public partial class SearchHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator, 
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) : ObservableViewModel<string>(provider, factory, mediator, publisher, subscriber, disposer);
