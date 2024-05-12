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
    string name,
    string description) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<Test>
{
    [ObservableProperty]
    private string? description = description;

    [ObservableProperty]
    private string? name = name;

    [ObservableProperty]
    private string named = $"{named}";

    [ObservableProperty]
    private bool selected;
    public IContentTemplate Template { get; set; } = template;

    public Task Handle(Test args, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}