using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template,
    NamedComponent named,
    int id,
    string name,
    string description,
    bool selected) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<ArchiveEventArgs<Item>>,
    IRemovable
{
    [ObservableProperty]
    private string? description = description;

    [ObservableProperty]
    private int id = id;

    [ObservableProperty]
    private string? name = name;

    [ObservableProperty]
    private string named = $"{named}";

    [ObservableProperty]
    private bool selected = selected;

    public IContentTemplate Template { get; set; } = template;

    public Task Handle(ArchiveEventArgs<Item> args)
    {
        Dispose();
        return Task.CompletedTask;
    }
}