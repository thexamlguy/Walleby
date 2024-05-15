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
    IHandler<ConfirmEventArgs<Item>, bool>

{
    [ObservableProperty]
    private bool immutable = immutable;

    public Task<bool> Handle(ConfirmEventArgs<Item> args, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }

    public void Invoke(ItemConfiguration args) => 
        args.Name = Value;
}
