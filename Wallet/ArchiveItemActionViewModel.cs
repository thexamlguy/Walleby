using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class ArchiveItemActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) : Observable(provider, factory, mediator, publisher, subscriber, disposer),
    IRemovable
{
    [RelayCommand]
    private void Invoke() => Publisher.Publish(Archive.As<Item>());
}