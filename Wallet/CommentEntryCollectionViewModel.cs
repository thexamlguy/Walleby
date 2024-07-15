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
    ItemEntryConfiguration<ICollection<Comment>> configuration,
    string key,
    ICollection<Comment> value,
    bool isConcealed,
    bool isRevealed,
    double width) : ItemEntryCollectionViewModel<ICommentEntryViewModel, ICollection<Comment>>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, isConcealed, isRevealed, width),
    INotificationHandler<CreateEventArgs<Comment>>
{
    public IContentTemplate Template { get; set; } = template;

    public Task Handle(CreateEventArgs<Comment> args)
    {
        if (args.Sender is Comment comment)
        {
            Insert<CommentEntryViewModel>(0, comment.DateTime, comment.Text);
            Value.Add(comment);
        }

        return Task.CompletedTask;
    }
}

