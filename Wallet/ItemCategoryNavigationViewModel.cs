using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemCategoryNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    string name,
    bool selected = false) :
    Observable(provider, factory, mediator, publisher, subscriber, disposer),
    ISelectable,
    IRemovable
{
    [ObservableProperty]
    private string name = name;

    [ObservableProperty]
    private bool selected = selected;

    [RelayCommand]
    public void Invoke() => Publisher.Publish(Notify.As(new ItemCategory<string>(Name)));
}
