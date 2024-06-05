using System.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ItemEntryConfiguration configuration,
    string? key = default,
    object? value = default) :
    Observable<string, object>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    IItemEntryViewModel
{
    protected override void OnValueChanged() => configuration.Value = Value;
}
