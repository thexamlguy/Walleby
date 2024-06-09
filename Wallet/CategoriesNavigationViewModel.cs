using Toolkit.Foundation;

namespace Wallet;

[Notification(typeof(CreateEventArgs<CategoryNavigationViewModel>), nameof(CategoriesNavigationViewModel))]
public partial class CategoriesNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string name) : FilterNavigationViewModel<CategoryNavigationViewModel>(provider, factory, mediator, publisher, subscriber, disposer, name)
{

}