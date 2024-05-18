using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ContainerNavigationViewModel :
    ObservableCollection<IContainerNavigationViewModel>,
    IMainNavigationViewModel,
    INotificationHandler<OpenedEventArgs<ContainerToken>>,
    INotificationHandler<ClosedEventArgs<ContainerToken>>,
    INotificationHandler<ActivatedEventArgs<ContainerToken>>,
    INotificationHandler<DeactivatedEventArgs<ContainerToken>>
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

    public Task Handle(OpenedEventArgs<ContainerToken> args)
    {
        Add<AllNavigationViewModel>("All");
        Add<StarredNavigationViewModel>("Starred");
        Add<ArchiveNavigationViewModel>("Archive");
        Add<CategoriesNavigationViewModel>("Categories");

        Opened = true;
        return Task.CompletedTask;
    }

    public Task Handle(ClosedEventArgs<ContainerToken> args)
    {
        Opened = true;
        Clear();

        return Task.CompletedTask;
    }

    public Task Handle(DeactivatedEventArgs<ContainerToken> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<ContainerToken> args) =>
        Task.FromResult(Activated = true);
}