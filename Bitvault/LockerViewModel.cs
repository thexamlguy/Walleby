using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class LockerViewModel :
    Observable
{
    [ObservableProperty]
    private string named;

    [ObservableProperty]
    private string filter;
    public LockerViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        NamedComponent named,
        string filter) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Named = $"{named}";
        Filter = filter;
    }

    public override async Task OnActivated()
    {
        Publisher.Publish(Activated.As<Locker>());
        await base.OnActivated();
    }

    public override async Task OnDeactivated()
    {
        Publisher.Publish(Deactivated.As<Locker>());
        await base.OnDeactivated();
    }
}