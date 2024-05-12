using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class AddItemActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    NamedComponent named) : ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer)
{

    [ObservableProperty]
    private string named = $"{named}";

    [RelayCommand]
    public async Task Invoke() => await Publisher.Publish(new Test());
}