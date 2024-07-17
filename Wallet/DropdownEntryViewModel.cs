using Toolkit.Foundation;

namespace Wallet;

public partial class DropdownEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string? value = default) : Observable<string>(provider, factory, mediator, publisher, subscriber, disposer, value);