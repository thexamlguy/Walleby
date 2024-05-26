using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ContainerViewModel :
    Observable
{
    [ObservableProperty]
    private string named;

    [ObservableProperty]
    private string filter;
    public ContainerViewModel(IServiceProvider provider,
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
        Publisher.Publish(Activated.As<Container>());
        await base.OnActivated();
    }

    public override async Task OnDeactivated()
    {
        Publisher.Publish(Deactivated.As<Container>());
        await base.OnDeactivated();
    }
}