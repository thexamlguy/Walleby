using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemMaskedTextEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    ItemState state,
    ItemEntryConfiguration configuration,
    string? pattern,
    string? key = default,
    object? value = default) : ItemEntryViewModel(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value)
{
    [ObservableProperty]
    private string? pattern = pattern;
}