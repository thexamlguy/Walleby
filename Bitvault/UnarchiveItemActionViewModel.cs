using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class UnarchiveItemActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer) : Observable(provider, factory, mediator, publisher, subscriber, disposer),
    IRemovable
{
    [RelayCommand]
    public void Invoke() => Publisher.Publish(Unarchive.As<Item>());
}

