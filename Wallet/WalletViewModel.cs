using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class WalletViewModel :
    Observable
{
    [ObservableProperty]
    private string named;

    [ObservableProperty]
    private string filter;

    public WalletViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        NamedComponent named,
        string filter) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Named = $"{named}";
        Filter = filter;
    }

    public override async Task Activated()
    {
        Publisher.Publish(Toolkit.Foundation.Activated.As<Wallet>());
        await base.Activated();
    }

    public override async Task Deactivated()
    {
        Publisher.Publish(Toolkit.Foundation.Deactivated.As<Wallet>());
        await base.Deactivated();
    }
}