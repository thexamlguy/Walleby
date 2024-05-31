using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ConfirmItemActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ItemState state) : Observable(provider, factory, mediator, publisher, subscriber, disposer)
{
    [RelayCommand]
    public void Invoke() => Publisher.Publish(Confirm.As<Item>(), 
        state is ItemState.New ? nameof(ItemState.New) : nameof(ItemState.Write));
}