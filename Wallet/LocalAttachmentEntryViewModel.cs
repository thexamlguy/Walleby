using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class LocalAttachmentEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string path,
    DateTimeOffset created,
    long size,
    string name) : Observable(provider, factory, mediator, publisher, subscriber, disposer),
    IAttachmentEntryViewModel
{
    [ObservableProperty]
    private DateTimeOffset created = created;

    [ObservableProperty]
    private string path = path;

    [ObservableProperty]
    private long size = size;

    [ObservableProperty]
    private string name = name;
}
