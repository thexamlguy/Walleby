using Toolkit.Foundation;

namespace Bitvault;

[Aggerate(nameof(ItemCollectionViewModel))]
public partial class ItemCollectionViewModel :
    ObservableCollection<ItemNavigationViewModel>,
    INotificationHandler<NotifyEventArgs<Filter>>,
    INotificationHandler<NotifyEventArgs<Search>>
{
    private ContainerViewModelConfiguration configuration;

    public ItemCollectionViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        ContainerViewModelConfiguration configuration,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        this.configuration = configuration with { Filter = filter };
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(NotifyEventArgs<Filter> args)
    {
        if (args.Value is Filter filter)
        {
            configuration = configuration with { Filter = filter.Value };
            BeginAggregation();
        }

        return Task.CompletedTask;
    }

    public Task Handle(NotifyEventArgs<Search> args)
    {
        if (args.Value is Search search)
        {
            configuration = configuration with { Query = search.Value };
            BeginAggregation();
        }

        return Task.CompletedTask;
    }

    protected override IAggerate OnPrepareAggregation(object? key) =>
        Aggerate.With<ItemNavigationViewModel, ContainerViewModelConfiguration>(configuration)
            with { Key = key };
}
