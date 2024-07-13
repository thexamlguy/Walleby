using Toolkit.Foundation;

namespace Wallet;

public partial class AttachmentEntryCollectionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    ItemState state,
    ItemEntryConfiguration configuration,
    string key,
    ICollection<Comment<(string, DateTimeOffset)>> value,
    bool isConcealed,
    bool isRevealed,
    double width) : ItemEntryCollectionViewModel<AttachmentEntryViewModel, ICollection<Comment<(string, DateTimeOffset)>>>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, isConcealed, isRevealed, width);
