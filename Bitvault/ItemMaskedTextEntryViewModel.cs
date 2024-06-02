using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemMaskedTextEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer) : Observable(provider, factory, mediator, publisher, subscriber, disposer),
    IItemEntryViewModel;