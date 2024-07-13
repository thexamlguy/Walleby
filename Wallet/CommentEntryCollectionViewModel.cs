using Toolkit.Foundation;

namespace Wallet;

public partial class CommentEntryCollectionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template,
    ItemState state,
    ItemEntryConfiguration configuration,
    string key,
    ICollection<Comment<(string, DateTimeOffset)>> value,
    bool isConcealed,
    bool isRevealed,
    double width) : ItemEntryCollectionViewModel<ICommentEntryViewModel, ICollection<Comment<(string, DateTimeOffset)>>>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, isConcealed, isRevealed, width),
    INotificationHandler<CreateEventArgs<Comment<string>>>
{
    public IContentTemplate Template { get; set; } = template;

    public Task Handle(CreateEventArgs<Comment<string>> args)
    {
        if (args.Sender is Comment<string> comment)
        {
            Insert<CommentEntryViewModel>(0, DateTimeOffset.Now, comment.Value);
        }

        return Task.CompletedTask;
    }
}

