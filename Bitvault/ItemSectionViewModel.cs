using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Notification(typeof(CreateEventArgs<IItemEntryViewModel>), nameof(Section))]
public partial class ItemSectionViewModel(ICollectionSynchronizer synchronizer, 
    IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template,
    string section) : ObservableCollection<IItemEntryViewModel>(synchronizer, provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private string section = section;

    public IContentTemplate Template { get; set; } = template;
}
