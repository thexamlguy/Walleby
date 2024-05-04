using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class VaultNavigationViewModel :
    ObservableCollectionViewModel<IVaultNavigationViewModel>,
    IMainNavigationViewModel,
    INotificationHandler<Opened>,
    INotificationHandler<Closed>
{
    [ObservableProperty]
    private bool expanded = true;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private bool opened;

    [ObservableProperty]
    private bool selected;

    public VaultNavigationViewModel(IServiceProvider provider,
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

    public Task Handle(Opened args, CancellationToken cancellationToken = default)
    {
        Add<AllNavigationViewModel>();
        Add<StarredNavigationViewModel>();
        Add<ArchiveNavigationViewModel>();
        Add<CategoriesNavigationViewModel>();

        Opened = true;
        return Task.CompletedTask;
    }

    public Task Handle(Closed args, CancellationToken cancellationToken = default)
    {
        Opened = true;
        Clear();

        return Task.CompletedTask;
    }
}