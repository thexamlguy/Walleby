using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Notification(typeof(CreateEventArgs<ItemCategoryNavigationViewModel>), nameof(ItemCategoryCollectionViewModel))]
public partial class ItemCategoryCollectionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template) :
    ObservableCollection<ItemCategoryNavigationViewModel>(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private IContentTemplate template = template;

    public override Task OnActivated()
    {
        Publisher.Publish(Notify.As(Factory.Create<WalletCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<BackActionViewModel>(),
        })));

        return base.OnActivated();
    }
}
