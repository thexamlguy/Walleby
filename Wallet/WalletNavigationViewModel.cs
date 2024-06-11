using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class WalletNavigationViewModel :
    ObservableCollection<IWalletNavigationViewModel>,
    IMainNavigationViewModel,
    INotificationHandler<OpenedEventArgs<Wallet>>,
    INotificationHandler<ClosedEventArgs<Wallet>>,
    INotificationHandler<ActivatedEventArgs<Wallet>>,
    INotificationHandler<DeactivatedEventArgs<Wallet>>,
    ISelectable
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

    public WalletNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        string name,
        bool selected) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Name = name;
        Selected = selected;
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(OpenedEventArgs<Wallet> args)
    {
        Add<AllNavigationViewModel>("All", 0);
        Add<StarredNavigationViewModel>("Starred", 0);
        Add<ArchiveNavigationViewModel>("Archive", 0);
        Add<CategoriesNavigationViewModel>("Categories", 0);

        Opened = true;
        return Task.CompletedTask;
    }

    public Task Handle(ClosedEventArgs<Wallet> args)
    {
        Opened = true;
        Clear();

        return Task.CompletedTask;
    }

    public Task Handle(DeactivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = true);
}