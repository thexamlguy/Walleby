using Toolkit.Foundation;

namespace Wallet;

public partial class ManageViewModel :
    ObservableCollection,
    INavigationViewModel
{
    public ManageViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;

        Add<CreateWalletNavigationViewModel>();
    }

    public IContentTemplate Template { get; set; }
}