using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Aggerate(nameof(ItemCollectionViewModel))]
public partial class ItemCollectionViewModel :
    ObservableCollection<ItemNavigationViewModel>,
    INotificationHandler<NotifyEventArgs<Filter>>,
    INotificationHandler<NotifyEventArgs<Search>>,
    IBackStack
{
    private LockerViewModelConfiguration configuration;

    public ItemCollectionViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        LockerViewModelConfiguration configuration,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        this.configuration = configuration with { Filter = filter };
    }

    public IContentTemplate Template { get; set; }

    public override Task OnDeactivated()
    {
        return base.OnDeactivated();
    }

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

    public override Task OnActivated()
    {
        Publisher.Publish(Notify.As(Factory.Create<LockerCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<CreateItemActionViewModel>(),
            Factory.Create<SearchLockerActionViewModel>(),
        })));

        return base.OnActivated();
    }

    protected override IAggerate OnAggerate(object? key) =>
        Aggerate.With<ItemNavigationViewModel, LockerViewModelConfiguration>(configuration)
            with { Key = key };
}
