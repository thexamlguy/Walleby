using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemCategoryNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string name) :
    Observable(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private string name = name;
}
