using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ArchiveItemActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) : ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer)
{
    [RelayCommand]
    public async Task Invoke() => await Publisher.Publish(Archive.As<Item>());
}
