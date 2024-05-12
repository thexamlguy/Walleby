using Toolkit.Foundation;

namespace Bitvault;

public partial class ContainerHeaderViewModel : ObservableCollectionViewModel<string, IDisposable>,
    INotificationHandler<Container<Filter<string>>>
{
    public ContainerHeaderViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;

        Add<AddItemActionViewModel>(scope: true);
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(Container<Filter<string>> args,
        CancellationToken cancellationToken = default)
    {
        if (args.Value is Filter<string> filter)
        {
            Value = filter.Value;
        }

        return Task.CompletedTask;
    }
}
