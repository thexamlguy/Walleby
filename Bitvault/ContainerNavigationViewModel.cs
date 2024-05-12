using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ContainerNavigationViewModel :
    ObservableCollectionViewModel<IContainerNavigationViewModel>,
    IMainNavigationViewModel,
    INotificationHandler<Container<Opened>>,
    INotificationHandler<Container<Closed>>,
    INotificationHandler<Container<Activated>>,
    INotificationHandler<Container<Deactivated>>
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private bool expanded = true;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private bool opened;

    [ObservableProperty]
    private bool selected;

    public ContainerNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template,
        string name) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Name = name;
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(Container<Opened> args,
        CancellationToken cancellationToken = default)
    {
        Add<AllNavigationViewModel>("All");
        Add<StarredNavigationViewModel>("Starred");
        Add<ArchiveNavigationViewModel>("Archive");
        Add<CategoriesNavigationViewModel>("Categories");

        Opened = true;
        return Task.CompletedTask;
    }

    public Task Handle(Container<Closed> args,
        CancellationToken cancellationToken = default)
    {
        Opened = true;
        Clear();

        return Task.CompletedTask;
    }

    public Task Handle(Container<Deactivated> args,
        CancellationToken cancellationToken = default) =>
            Task.FromResult(Activated = false);

    public Task Handle(Container<Activated> args,
        CancellationToken cancellationToken = default) =>
            Task.FromResult(Activated = true);
}