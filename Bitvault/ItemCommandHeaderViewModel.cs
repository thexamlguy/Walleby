using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemCommandHeaderViewModel : ObservableCollectionViewModel
{
    public ItemCommandHeaderViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator, 
        IPublisher publisher, 
        ISubscriber subscriber, 
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Add<ConfirmItemActionViewModel>();
        Add<DismissItemActionViewModel>();
        Add<EditItemActionViewModel>();
        Add<DeleteItemActionViewModel>();
        Add<ArchiveItemActionViewModel>();

        Template = template;
    }

    public IContentTemplate Template { get; set; }
}