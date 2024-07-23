using Toolkit.Foundation;

namespace Wallet;

public partial class CommentEntryCollectionViewModel : ItemEntryCollectionViewModel<ICommentEntryViewModel, ICollection<Comment>>,
    INotificationHandler<CreateEventArgs<Comment>>
{
    public CommentEntryCollectionViewModel(IServiceProvider provider,
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
        double width) : base(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, isConcealed, isRevealed, width)
    {
        Template = template;
        UpdateState();
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(CreateEventArgs<Comment> args)
    {
        if (args.Sender is Comment comment)
        {
            Insert<CommentEntryViewModel>(0, comment.DateTime, comment.Text);
            Value.Add(comment);
        }

        return Task.CompletedTask;
    }

    protected override void OnStateChanged() => 
        UpdateState();

    private void UpdateState()
    {
        if (State is ItemState.Write or ItemState.New)
        {
            Add<CreateCommentEntryViewModel>();
        }
        else
        {
            if (Count > 0)
            {
                RemoveAt(Count - 1);
            }
        }
    }
}