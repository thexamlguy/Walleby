using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class LockerNavigationViewModel :
    ObservableCollection<ILockerNavigationViewModel>,
    IMainNavigationViewModel,
    INotificationHandler<OpenedEventArgs<Locker>>,
    INotificationHandler<ClosedEventArgs<Locker>>,
    INotificationHandler<ActivatedEventArgs<Locker>>,
    INotificationHandler<DeactivatedEventArgs<Locker>>
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

    public LockerNavigationViewModel(IServiceProvider provider,
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

    public Task Handle(OpenedEventArgs<Locker> args)
    {
        Add<AllNavigationViewModel>("All");
        Add<StarredNavigationViewModel>("Starred");
        Add<ArchiveNavigationViewModel>("Archive");
        Add<CategoriesNavigationViewModel>("Categories");

        Opened = true;
        return Task.CompletedTask;
    }

    public Task Handle(ClosedEventArgs<Locker> args)
    {
        Opened = true;
        Clear();

        return Task.CompletedTask;
    }

    public Task Handle(DeactivatedEventArgs<Locker> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Locker> args) =>
        Task.FromResult(Activated = true);
}