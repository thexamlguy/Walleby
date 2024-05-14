using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemViewModel : 
    ObservableCollectionViewModel<IItemViewModel>,
    INotificationHandler<ConfirmEventArgs<Item>>
{
    [ObservableProperty]
    private int? id;

    [ObservableProperty]
    private string named;

    [ObservableProperty]
    private bool immutable;

    public ItemViewModel(IServiceProvider provider, 
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher, 
        ISubscription subscriber, 
        IDisposer disposer,
        IContentTemplate template,
        NamedComponent named,
        bool immutable = true,
        int? id = null,
        string? name = null) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Named = $"{named}";
        Id = id;
        Immutable = immutable;

        Add<ItemHeaderViewModel>(immutable, name ?? "");
    }

    public IContentTemplate Template { get; set; }

    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        ItemConfiguration configuration = new();
        foreach (IItemViewModel item in this)
        {
            item.Invoke(configuration);
        }
        
        await Mediator.Handle<CreateEventArgs<ItemConfiguration>, bool>(Create.As(configuration));
    }
}
