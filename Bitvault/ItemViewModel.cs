using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Notification(typeof(ConfirmEventArgs<Item>), nameof(Create))]
public partial class ItemViewModel :
    ObservableCollection<IItemEntryViewModel>,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>

{
    [ObservableProperty]
    private bool archived;

    [ObservableProperty]
    private bool favourite;

    [ObservableProperty]
    private ItemState state;

    [ObservableProperty]
    private string named;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private bool fromCategory;

    public ItemViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        NamedComponent named,
        ItemState state = ItemState.Read,
        bool fromCategory = false,
        string name = "",
        bool favourite = false,
        bool archived = false) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Named = $"{named}";
        State = state;
        FromCategory = fromCategory;
        Favourite = favourite;
        Archived = archived;
        Name = name;

        Add<ItemHeaderViewModel>(state, name);
        Add<ItemContentViewModel>(state);
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(UpdateEventArgs<Item> args)
    {
        Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<ConfirmItemActionViewModel>(ItemState.Write),
            Factory.Create<DismissItemActionViewModel>(),
        })));

        State = ItemState.Write;
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new
            List<IDisposable>())));

        base.Dispose();
    }

    public Task Handle(CancelEventArgs<Item> args)
    {
        Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<EditItemActionViewModel>(),
            Factory.Create<ArchiveItemActionViewModel>(),
        })));

        State = ItemState.Read;
        return Task.CompletedTask;
    }

    public Task Handle(ConfirmEventArgs<Item> args)
    {
        Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<FavouriteItemActionViewModel>(Favourite),
            Factory.Create<EditItemActionViewModel>(),
            Factory.Create<ArchiveItemActionViewModel>(),
        })));

        State = ItemState.Read;
        return Task.CompletedTask;
    }

    public override Task OnActivated()
    {
        if (Archived)
        {
            Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
            {
                Factory.Create<UnarchiveItemActionViewModel>(),
            })));
        }
        else
        {
            if (State is ItemState.Write or ItemState.New)
            {
                Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
                {
                    Factory.Create<ConfirmItemActionViewModel>(State),
                    Factory.Create<DismissItemActionViewModel>(),
                })));
            }
            else
            {
                Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
                {
                    Factory.Create<FavouriteItemActionViewModel>(Favourite),
                    Factory.Create<EditItemActionViewModel>(),
                    Factory.Create<ArchiveItemActionViewModel>(),
                })));
            }
        }

        return base.OnActivated();
    }
}