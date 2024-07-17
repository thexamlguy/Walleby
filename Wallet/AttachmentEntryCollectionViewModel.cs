using Toolkit.Foundation;

namespace Wallet;

public partial class AttachmentEntryCollectionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    ItemState state,
    IItemEntryConfiguration<ICollection<Comment>> configuration,
    string key,
    ICollection<Comment> value,
    bool isConcealed,
    bool isRevealed,
    double width) : ItemEntryCollectionViewModel<AttachmentEntryViewModel, ICollection<Comment>>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, isConcealed, isRevealed, width);