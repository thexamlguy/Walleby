using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    bool immutable,
    string? value = null) : ObservableViewModel<string, string>(provider, factory, mediator, publisher, subscriber, disposer, value),
    IItemViewModel
{
    [ObservableProperty]
    private bool immutable = immutable;

    public void Invoke(ItemConfiguration args) => 
        args.Name = Value;
}
