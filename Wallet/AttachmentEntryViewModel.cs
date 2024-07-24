using Toolkit.Foundation;

namespace Wallet;

public partial class AttachmentEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    Attachment? value = default) : Observable<Attachment>(provider, factory, mediator, publisher, subscriber, disposer, value);