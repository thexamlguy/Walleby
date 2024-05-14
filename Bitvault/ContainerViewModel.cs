using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;


[Enumerate(nameof(ContainerViewModel))]
public partial class ContainerViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template,
    NamedComponent named,
    string? filter = null) : ObservableCollectionViewModel<ItemNavigationViewModel>(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<RequestEventArgs<Filter<string>>>
{
    [ObservableProperty]
    private string? filter = filter;

    [ObservableProperty]
    private string named = $"{named}";

    public IContentTemplate Template { get; set; } = template;

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

    public Task Handle(RequestEventArgs<Filter<string>> args)
    {
        if (args.Value is Filter<string> filter)
        {
            Filter = filter.Value;
            Enumerate();
        }

        return Task.CompletedTask;
    }

    protected override IEnumerate PrepareEnumeration(object? key) =>
        EnumerateEventArgs<ItemNavigationViewModel>.With(new ContainerViewModelConfiguration { Filter = Filter }) with { Key = key };
}