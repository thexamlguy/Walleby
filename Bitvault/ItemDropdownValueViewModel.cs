using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemDropdownValueViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer, 
    string? value = null) : Observable<string>(provider, factory, mediator, publisher, subscriber, disposer, value);
