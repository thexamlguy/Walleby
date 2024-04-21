using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class VaultNavigationViewModel :
    ObservableCollectionViewModel<IMainNavigationViewModel>,
    IMainNavigationViewModel
{
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

        Add<AllNavigationViewModel>();
        Add<StarredNavigationViewModel>();
        Add<ArchiveNavigationViewModel>();
        Add<CategoriesNavigationViewModel>();
    }

    public IContentTemplate Template { get; set; }
}