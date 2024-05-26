using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ContainerNavigationViewModel :
    ObservableCollection<IContainerNavigationViewModel>,
    IMainNavigationViewModel,
    INotificationHandler<OpenedEventArgs<Container>>,
    INotificationHandler<ClosedEventArgs<Container>>,
    INotificationHandler<ActivatedEventArgs<Container>>,
    INotificationHandler<DeactivatedEventArgs<Container>>
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
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        string name) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Name = name;
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(OpenedEventArgs<Container> args)
    {
        Add<AllNavigationViewModel>("All");
        Add<StarredNavigationViewModel>("Starred");
        Add<ArchiveNavigationViewModel>("Archive");
        Add<CategoriesNavigationViewModel>("Categories");

        Opened = true;
        return Task.CompletedTask;
    }

    public Task Handle(ClosedEventArgs<Container> args)
    {
        Opened = true;
        Clear();

        return Task.CompletedTask;
    }

    public Task Handle(DeactivatedEventArgs<Container> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Container> args) =>
        Task.FromResult(Activated = true);
}