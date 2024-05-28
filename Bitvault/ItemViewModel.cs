using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

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
    private bool immutable;

    [ObservableProperty]
    private string named;

    [ObservableProperty]
    private string name;

    public ItemViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        NamedComponent named,
        string name = "",
        bool immutable = true,
        bool favourite = false,
        bool archived = false) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Named = $"{named}";
        Template = template;
        Immutable = immutable;
        Favourite = favourite;
        Archived = archived;
        Name = name;

        Add<ItemHeaderViewModel>(immutable, name);
        Add<ItemContentViewModel>(immutable);
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(UpdateEventArgs<Item> args)
    {
        Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<ConfirmItemActionViewModel>(),
            Factory.Create<DismissItemActionViewModel>(),
        })));

        return Task.CompletedTask;
    }

    public Task Handle(CancelEventArgs<Item> args)
    {
        Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<EditItemActionViewModel>(),
            Factory.Create<ArchiveItemActionViewModel>(),
        })));

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

        return Task.CompletedTask;
    }

    public override Task OnActivated()
    {
        if (!Immutable)
        {
            Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
            {
                Factory.Create<ConfirmItemActionViewModel>(),
                Factory.Create<DismissItemActionViewModel>(),
            })));
        }
        else if (Archived)
        {
            Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
            {
                Factory.Create<UnarchiveItemActionViewModel>(),
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

        return base.OnActivated();
    }
}