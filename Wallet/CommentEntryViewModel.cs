using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class CommentEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    DateTimeOffset created,
    string comment) : Observable(provider, factory, mediator, publisher, subscriber, disposer),
    ICommentEntryViewModel
{
    [ObservableProperty]
    private DateTimeOffset created = created;

    [ObservableProperty]
    private string comment = comment;
}