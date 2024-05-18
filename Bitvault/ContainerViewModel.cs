using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Toolkit.Foundation;

namespace Bitvault;

public class ItemCommandCollection : ObservableCollection
{
    public void Add<TItem>(IDisposable diposer)
    {

    }
}

[Enumerate(nameof(ContainerViewModel))]
public partial class ContainerViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template,
    NamedComponent named,
    ContainerViewModelConfiguration configuration) : Toolkit.Foundation.ObservableCollection<ItemNavigationViewModel>(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<NotifyEventArgs<Filter>>,
    INotificationHandler<NotifyEventArgs<Search>>
{
    [ObservableProperty]
    private string named = $"{named}";

    public IContentTemplate Template { get; set; } = template;

    public override async Task OnActivated()
    {
        Publisher.Publish(Activated.As<ContainerToken>());
        await base.OnActivated();
    }

    public override async Task OnDeactivated()
    {
        Publisher.Publish(Deactivated.As<ContainerToken>());
        await base.OnDeactivated();
    }

    public Task Handle(NotifyEventArgs<Filter> args)
    {
        if (args.Value is Filter filter)
        {
            configuration = configuration with { Filter = filter.Value };
            Enumerate();
        }

        return Task.CompletedTask;
    }
    public Task Handle(NotifyEventArgs<Search> args)
    {
        if (args.Value is Search search)
        {
            configuration = configuration with { Query = search.Value };
            Enumerate();
        }

        return Task.CompletedTask;
    }

    protected override IEnumerate PrepareEnumeration(object? key) =>
        EnumerateEventArgs<ItemNavigationViewModel>.With(configuration) with { Key = key };
}