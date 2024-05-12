using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class AddItemViewModel : 
    ObservableCollectionViewModel<IItemViewModel>,
    INotificationHandler<Confirm<Item>>
{
    [ObservableProperty]
    private string named;

    public AddItemViewModel(IServiceProvider provider, 
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher, 
        ISubscriber subscriber, 
        IDisposer disposer,
        IContentTemplate template,
        NamedComponent named) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Named = $"{named}";

        Add<ItemHeaderViewModel>();
    }

    public IContentTemplate Template { get; set; }

    public async Task Handle(Confirm<Item> args, CancellationToken cancellationToken = default)
    {
        ItemConfiguration configuration = new();
        foreach (IItemViewModel item in this)
        {
            item.Invoke(configuration);
        }

         await Mediator.Handle<Create<ItemConfiguration>, bool>(Create.As(configuration), cancellationToken);
    }
}
