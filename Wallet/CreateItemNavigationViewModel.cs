using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class CreateItemNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    NamedComponent named) : Observable(provider, factory, mediator, publisher, subscriber, disposer),
    INavigationViewModel
{
    [ObservableProperty]
    private bool isSelected;

    [ObservableProperty]
    private string named = $"{named}";
}