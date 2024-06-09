using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class EmptyItemCollectionViewModel(IServiceProvider provider,
    IServiceFactory factory, 
    IMediator mediator, 
    IPublisher publisher,
    ISubscription subscriber, 
    IDisposer disposer,
    NamedComponent named) : Observable(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private string named = $"{named}";
}
