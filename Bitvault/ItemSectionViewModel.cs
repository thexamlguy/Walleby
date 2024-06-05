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
    IContentTemplate template,
    ISynchronizationCollection<ItemSectionViewModel> synchronization,
    string section) : ObservableCollection<IItemEntryViewModel>(provider, factory, mediator, publisher, subscriber, disposer),
    IHandler<ConfirmEventArgs<ItemSection>, (int, string)>,
    IIndexable
{
    [ObservableProperty]
    private string section = section;

    public IContentTemplate Template { get; set; } = template;

    public int Index => synchronization.IndexOf(this);

    public Task<(int, string)> Handle(ConfirmEventArgs<ItemSection> args,
        CancellationToken cancellationToken) => Task.FromResult((0, Section));
}
