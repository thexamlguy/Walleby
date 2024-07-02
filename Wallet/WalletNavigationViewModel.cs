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
    private bool isExpanded = true;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private bool isOpened;

    [ObservableProperty]
    private bool isSelected;

    [ObservableProperty]
    private bool isActivated;

    [ObservableProperty]
    private IImageDescriptor? imageDescriptor;

    public WalletNavigationViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template,
        string name,
        IImageDescriptor? imageDescriptor = default,
        bool isSelected = false) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Name = name;
        ImageDescriptor = imageDescriptor;
        IsSelected = isSelected;
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(OpenedEventArgs<Wallet> args)
    {
        Add<CreateItemNavigationViewModel>();
        Add<AllNavigationViewModel>("All", 0);
        Add<FavouritesNavigationViewModel>("Favourites", 0);
        Add<ArchiveNavigationViewModel>("Archive", 0);
        Add<CategoriesNavigationViewModel>("Categories", 0);
        IsOpened = true;

        Publisher.Publish(Changed.As<Item>());

        return Task.CompletedTask;
    }

    public Task Handle(ClosedEventArgs<Wallet> args)
    {
        IsOpened = true;
        Clear();

        return Task.CompletedTask;
    }

    public Task Handle(DeactivatedEventArgs<Wallet> args)
    {
        IsActivated = false;
        return Task.CompletedTask;
    }

    public Task Handle(ActivatedEventArgs<Wallet> args)
    {
        IsActivated = true;
        return Task.CompletedTask;
    }
}