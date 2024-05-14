using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class DeleteItemActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer) : ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer)
{
    [RelayCommand]
    public void Invoke() => Publisher.Publish(Delete.As<Item>());
}
