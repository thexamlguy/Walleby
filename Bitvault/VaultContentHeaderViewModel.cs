using Toolkit.Foundation;

namespace Bitvault;

public partial class VaultContentHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) : ObservableViewModel<string, string>(provider, factory, mediator, publisher, subscriber, disposer),
    IVaultContentEntryViewModel
{
    public void Invoke(VaultContentConfiguration args) => 
        args.Name = Value;
}
