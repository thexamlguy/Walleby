using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class VaultContentNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template,
    string name,
    string description) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private bool selected;

    [ObservableProperty]
    private string? name = name;

    [ObservableProperty]
    private string? description = description;

    public IContentTemplate Template { get; set; } = template;
}