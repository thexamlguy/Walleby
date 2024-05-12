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
    string description) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer)
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
    private bool selected;
    public IContentTemplate Template { get; set; } = template;
}