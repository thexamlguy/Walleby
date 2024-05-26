using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Aggerate(nameof(ContainerViewModel))]
public partial class ContainerViewModel : ObservableCollection<ItemNavigationViewModel>,
    INotificationHandler<NotifyEventArgs<Filter>>,
    INotificationHandler<NotifyEventArgs<Search>>
{
    [ObservableProperty]
    private string named;

    public ContainerViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        ContainerViewModelConfiguration configuration,
        NamedComponent named,
        string? filter = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Named = $"{named}";

        this.configuration = configuration with { Filter = filter };
    }

    private ContainerViewModelConfiguration configuration;

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

    public override async Task OnActivated()
    {
        Publisher.Publish(Activated.As<Container>());
        await base.OnActivated();
    }

    public override async Task OnDeactivated()
    {
        Publisher.Publish(Deactivated.As<Container>());
        await base.OnDeactivated();
    }
    protected override IAggerate OnPrepareAggregation(object? key) =>
        Aggerate.With<ItemNavigationViewModel, ContainerViewModelConfiguration>(configuration) with { Key = key };
}