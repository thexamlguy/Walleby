using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class SearchContainerActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    int index) : Observable<string>(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private int index = index;

    [RelayCommand]
    public void Invoke() => Publisher.Publish(Notify.As(new Search(Value)),
        nameof(ContainerViewModel));
}

