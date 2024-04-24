using Avalonia.Styling;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ManageViewModel :
    ObservableCollectionViewModel,
    IMainNavigationViewModel
{
    public ManageViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        Template = template;

        Add<CreateVaultNavigationViewModel>();
    }

    public IContentTemplate Template { get; set; }
}