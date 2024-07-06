using Toolkit.Foundation;

namespace Wallet;

public partial class PasswordEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IClipboardWriter clipboardWriter,
    ItemState state,
    ItemEntryConfiguration configuration,
    string key,
    string value,
    bool isConcealed,
    bool isRevealed,
    double width) : ItemEntryViewModel<string>(provider, factory, mediator, publisher, subscriber, disposer, clipboardWriter, state, configuration, key, value, isConcealed, isRevealed, width);