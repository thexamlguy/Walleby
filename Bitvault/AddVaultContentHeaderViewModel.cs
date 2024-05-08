using Toolkit.Foundation;

namespace Bitvault;

public partial class AddVaultContentHeaderViewModel : ObservableCollectionViewModel
{
    public AddVaultContentHeaderViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator, 
        IPublisher publisher, 
        ISubscriber subscriber, 
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Add<ConfirmVaultContentActionViewModel>();
        Add<DismissVaultContentActionViewModel>();

        Template = template;
    }

    public IContentTemplate Template { get; set; }
}