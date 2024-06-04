﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Notification(typeof(CreateEventArgs<ItemNavigationViewModel>), nameof(ItemCollectionViewModel))]
[Notification(typeof(InsertEventArgs<ItemNavigationViewModel>), nameof(ItemCollectionViewModel))]
[Notification(typeof(MoveToEventArgs<ItemNavigationViewModel>), nameof(ItemCollectionViewModel))]
[Notification(typeof(NotifyEventArgs<Search<string>>), nameof(ItemCollectionViewModel))]
public partial class ItemCollectionViewModel :
    ObservableCollection<ItemNavigationViewModel>,
    INotificationHandler<NotifyEventArgs<Filter>>,
    INotificationHandler<NotifyEventArgs<Search<string>>>,
    IBackStack
{
    [ObservableProperty]
    public string? named;

    private LockerViewModelConfiguration configuration;

    public ItemCollectionViewModel(ICollectionSynchronizer synchronizer, 
        IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        NamedComponent named,
        LockerViewModelConfiguration configuration,
        string? filter = null) : base(synchronizer, provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Named = $"{named}";

        this.configuration = configuration with { Filter = filter };
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(NotifyEventArgs<Filter> args)
    {
        if (args.Value is Filter filter)
        {
            configuration = configuration with { Filter = filter.Value };
            Fetch(true);
        }

        return Task.CompletedTask;
    }

    public Task Handle(NotifyEventArgs<Search<string>> args)
    {
        if (args.Value is Search<string> search)
        {
            configuration = configuration with { Query = search.Value };
            Fetch(true);
        }

        return Task.CompletedTask;
    }

    public override Task OnActivated()
    {
        Publisher.Publish(Notify.As(Factory.Create<LockerCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<CreateItemActionViewModel>(),
            Factory.Create<SearchLockerActionViewModel>(),
        })));

        return base.OnActivated();
    }

    protected override AggregateExpression BuildAggregateExpression() =>
        new(Aggregate.As<ItemNavigationViewModel, LockerViewModelConfiguration>(configuration));
}
