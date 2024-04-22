using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class VaultNavigationViewModel :
    ObservableCollectionViewModel<IMainNavigationViewModel>,
    IMainNavigationViewModel,
    INotificationHandler<Unlocked>,
    INotificationHandler<Locked>
{
    [ObservableProperty]
    private bool locked;

    [ObservableProperty]
    private string name;

    public VaultNavigationViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template,
        string name) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        Template = template;
        Name = name;



    }

    public IContentTemplate Template { get; set; }

    public Task Handle(Unlocked args, CancellationToken cancellationToken = default)
    {
        Locked = true;

        Add<AllNavigationViewModel>();
        Add<StarredNavigationViewModel>();
        Add<ArchiveNavigationViewModel>();
        Add<CategoriesNavigationViewModel>();

        return Task.CompletedTask;

    }

    public Task Handle(Locked args, CancellationToken cancellationToken = default)
    {
        Locked = true;
        Clear();

        return Task.CompletedTask;
    }
}