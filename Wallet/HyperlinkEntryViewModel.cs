using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class HyperlinkEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    ItemState state,
    ItemEntryConfiguration configuration,
    string key,
    string value,
    double width,
    bool isConcealed,
    bool isRevealed) : ItemEntryViewModel<string>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, isConcealed, isRevealed, width)
{
    [RelayCommand]
    private void Invoke() => Publisher.Publish(Create.As(new Hyperlink(Value)));
}