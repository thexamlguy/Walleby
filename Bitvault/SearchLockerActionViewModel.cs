using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class SearchLockerActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer) : Observable<string>(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private int index = 2;

    [RelayCommand]
    public void Invoke() => Publisher.Publish(Notify.As(new Search(Value)),
        nameof(ItemCollectionViewModel));
}

public partial class BackContainerActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer) : Observable<string>(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private int index = 2;

    [RelayCommand]
    public void Invoke() => Publisher.Publish(Notify.As(new Search(Value)),
        nameof(ItemCollectionViewModel));
}