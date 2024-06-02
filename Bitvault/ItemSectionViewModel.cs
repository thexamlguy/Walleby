using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Notification(typeof(CreateEventArgs<IItemEntryViewModel>), nameof(Section))]
public partial class ItemSectionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string section) : ObservableCollection<IItemEntryViewModel>(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private string section = section;
}
