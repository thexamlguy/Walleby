using Toolkit.Foundation;

namespace Wallet;

public partial class DropdownValueViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer, 
    string? value = null) : Observable<string>(provider, factory, mediator, publisher, subscriber, disposer, value);
