using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Notification(typeof(CreateEventArgs<ItemCategoryNavigationViewModel>), nameof(ItemCategoryCollectionViewModel))]
public partial class ItemCategoryCollectionViewModel(ICollectionSynchronizer synchronizer, 
    IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template) :
    ObservableCollection<ItemCategoryNavigationViewModel>(synchronizer, provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private IContentTemplate template = template;

    public override Task OnActivated()
    {
        Publisher.Publish(Notify.As(Factory.Create<LockerCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<BackActionViewModel>(),
        })));

        return base.OnActivated();
    }
}
