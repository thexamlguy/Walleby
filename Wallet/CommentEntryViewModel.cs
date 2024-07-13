using Toolkit.Foundation;

namespace Wallet;

public partial class CommentEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    DateTimeOffset key,
    string? value = default) : Observable<DateTimeOffset, string>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    ICommentEntryViewModel;