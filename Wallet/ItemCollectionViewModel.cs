using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

[Notification(typeof(CreateEventArgs<ItemNavigationViewModel>), nameof(ItemCollectionViewModel))]
[Notification(typeof(InsertEventArgs<ItemNavigationViewModel>), nameof(ItemCollectionViewModel))]
[Notification(typeof(MoveToEventArgs<ItemNavigationViewModel>), nameof(ItemCollectionViewModel))]
[Notification(typeof(NotifyEventArgs<Search<string>>), nameof(ItemCollectionViewModel))]
public partial class ItemCollectionViewModel :
    ObservableCollection<ItemNavigationViewModel>,
    INotificationHandler<NotifyEventArgs<Filter>>,
    INotificationHandler<NotifyEventArgs<Search<string>>>,
    IKeepAlive
{
    [ObservableProperty]
    public string? named;

    private ItemCollectionConfiguration configuration;

    public ItemCollectionViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template,
        NamedComponent named,
        ItemCollectionConfiguration configuration,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Named = $"{named}";

        this.configuration = configuration with { Filter = filter };
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(NotifyEventArgs<Filter> args)
    {
        if (args.Sender is Filter filter)
        {
            configuration = configuration with { Filter = filter.Value };
            Synchronize(true);
        }

        return Task.CompletedTask;
    }

    public Task Handle(NotifyEventArgs<Search<string>> args)
    {
        if (args.Sender is Search<string> search)
        {
            configuration = configuration with { Query = search.Value };
            Synchronize(true);
        }

        return Task.CompletedTask;
    }

    public override Task Activated()
    {
        Publisher.Publish(Notify.As(Factory.Create<WalletCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<SearchWalletActionViewModel>(),
        })));

        return base.Activated();
    }

    protected override SynchronizeExpression BuildAggregateExpression() =>
        new(Toolkit.Foundation.Synchronize.As<ItemNavigationViewModel, ItemCollectionConfiguration>(configuration));
}
