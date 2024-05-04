using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class CategoriesNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer),
    IVaultNavigationViewModel
{
    [ObservableProperty]
    private bool selected;
}