using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template,
    NamedComponent named,
    int id,
    string name,
    string description,
    bool selected,
    bool favourite = false,
    bool archived = false) :
    Observable(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<ArchiveEventArgs<Item>>,
    INotificationHandler<UnarchiveEventArgs<Item>>,
    INotificationHandler<FavouriteEventArgs<Item>>,
    INotificationHandler<UnfavouriteEventArgs<Item>>,
    ISelectable,
    IRemovable
{
    [ObservableProperty]
    private bool archived = archived;

    [ObservableProperty]
    private string? description = description;

    [ObservableProperty]
    private bool favourite = favourite;

    [ObservableProperty]
    private int id = id;

    [ObservableProperty]
    private string? name = name;

    [ObservableProperty]
    private string named = $"{named}";

    [ObservableProperty]
    private bool selected = selected;

    public IContentTemplate Template { get; set; } = template;

    public Task Handle(ArchiveEventArgs<Item> args) => 
        Task.Run(Dispose);

    public Task Handle(UnarchiveEventArgs<Item> args) => 
        Task.Run(Dispose);

    public Task Handle(FavouriteEventArgs<Item> args) => 
        Task.FromResult(Favourite = true);

    public Task Handle(UnfavouriteEventArgs<Item> args) =>
        Task.FromResult(Favourite = false);
}