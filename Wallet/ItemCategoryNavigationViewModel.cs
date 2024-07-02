using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemCategoryNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    string name,
    bool isSelected = false) :
    Observable(provider, factory, mediator, publisher, subscriber, disposer),
    ISelectable,
    IRemovable
{
    [ObservableProperty]
    private string name = name;

    [ObservableProperty]
    private bool isSelected = isSelected;

    [RelayCommand]
    private void Invoke() => Publisher.Publish(Notify.As(new ItemCategory<string>(Name)));
}
