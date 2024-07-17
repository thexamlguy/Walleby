using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

[Notification(typeof(CreateEventArgs<ItemNavigationViewModel>), nameof(Filter))]
[Notification(typeof(InsertEventArgs<ItemNavigationViewModel>), nameof(Filter))]
[Notification(typeof(MoveToEventArgs<ItemNavigationViewModel>), nameof(Filter))]
[Notification(typeof(NotifyEventArgs<Search<string>>), nameof(ItemNavigationCollectionViewModel))]
public partial class ItemNavigationCollectionViewModel :
    ObservableCollection<ItemNavigationViewModel>,
    INotificationHandler<NotifyEventArgs<Filter>>,
    INotificationHandler<NotifyEventArgs<Search<string>>>,
    IKeepAlive
{
    [ObservableProperty]
    public string? named;

    private ItemNavigationCollectionConfiguration configuration;

    public ItemNavigationCollectionViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template,
        NamedComponent named,
        ItemNavigationCollectionConfiguration configuration,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Named = $"{named}";
        Filter = filter;

        this.configuration = configuration with { Filter = filter };
    }

    [ObservableProperty]
    private string? filter;

    public IContentTemplate Template { get; set; }

    public Task Handle(NotifyEventArgs<Filter> args)
    {
        if (args.Sender is Filter filter)
        {
            configuration = configuration with { Filter = filter.Value };
            Activate(true);
        }

        return Task.CompletedTask;
    }

    public Task Handle(NotifyEventArgs<Search<string>> args)
    {
        if (args.Sender is Search<string> search)
        {
            configuration = configuration with { Query = search.Value };
            Activate(true);
        }

        return Task.CompletedTask;
    }

    public override Task OnActivated()
    {
        Publisher.Publish(Notify.As(Factory.Create<WalletCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<SearchWalletActionViewModel>(),
        })));

        return base.OnActivated();
    }

    protected override ActivationBuilder ActivationBuilder() =>
        new(Activation.As<ItemNavigationViewModel, ItemNavigationCollectionConfiguration>(configuration));
}