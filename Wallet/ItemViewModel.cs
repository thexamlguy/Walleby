using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemViewModel :
    ObservableCollection<IItemViewModel>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>
{
    [ObservableProperty]
    private bool archived;

    [ObservableProperty]
    private bool favourite;

    [ObservableProperty]
    private bool fromCategory;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string named;

    [ObservableProperty]
    private ItemState state;

    public ItemViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template,
        NamedComponent named,
        IDecoratorService<ItemHeaderConfiguration> itemHeaderConfigurationDecorator,
        string name = "",
        string category = "",
        ImageDescriptor? imageDescriptor = null,
        bool fromCategory = false,
        bool favourite = false,
        bool archived = false,
        ItemState state = ItemState.Read) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Named = $"{named}";
        State = state;
        FromCategory = fromCategory;
        Favourite = favourite;
        Archived = archived;
        Name = name;

        ItemHeaderConfiguration configuration = new()
        {
            Name = name,
            Category = category,
        };

        itemHeaderConfigurationDecorator.Set(configuration);

        Add<ItemHeaderViewModel>(configuration, state, name, imageDescriptor);
        Add<ItemContentViewModel>();
    }

    public IContentTemplate Template { get; set; }

    public override void Dispose()
    {
        GC.SuppressFinalize(this);
        Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new
            List<IDisposable>())));

        base.Dispose();
    }

    public Task Handle(UpdateEventArgs<Item> args)
    {
        Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
        {
            Factory.Create<ConfirmItemActionViewModel>(),
            Factory.Create<DismissItemActionViewModel>(),
        })));

        State = ItemState.Write;
        return Task.CompletedTask;
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

        Publisher.Publish(Confirm.As<Item>(),
            State is ItemState.New ? nameof(ItemState.New) : nameof(ItemState.Write));

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
                Factory.Create<DeleteItemActionViewModel>(),
            })));
        }
        else
        {
            if (State is ItemState.Write or ItemState.New)
            {
                Publisher.Publish(Notify.As(Factory.Create<ItemCommandHeaderCollection>(new List<IDisposable>
                {
                    Factory.Create<ConfirmItemActionViewModel>(),
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