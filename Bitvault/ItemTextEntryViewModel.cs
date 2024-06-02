using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemTextEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string? key = default,
    string? value = default) : Observable<string, string>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    IItemEntryViewModel;
