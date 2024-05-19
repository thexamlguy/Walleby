using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Aggerate(nameof(ItemViewModel))]
public partial class ItemViewModel : 
    ObservableCollection<IItemEntryViewModel>,
    INotificationHandler<EditEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>
{
    [ObservableProperty]
    private bool archived;

    [ObservableProperty]
    private bool favourite;

    [ObservableProperty]
    private bool immutable;

    public ItemViewModel(IServiceProvider provider, 
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher, 
        ISubscription subscriber, 
        IDisposer disposer,
        IContentTemplate template,
        bool immutable = true,
        bool favourite = false,
        bool archived = false) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Immutable = immutable;
        Favourite = favourite;
        Archived = archived;
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(EditEventArgs<Item> args)
    {
        Publisher.Publish(Notify.As(Factory.Create<CommandCollection>(new List<IDisposable>
        {
            Factory.Create<ConfirmItemActionViewModel>(),
            Factory.Create<DismissItemActionViewModel>(),
        })));

        return Task.CompletedTask;
    }

    public Task Handle(CancelEventArgs<Item> args)
    {
        Publisher.Publish(Notify.As(Factory.Create<CommandCollection>(new List<IDisposable>
        {
            Factory.Create<EditItemActionViewModel>(),
            Factory.Create<ArchiveItemActionViewModel>(),
        })));

        return Task.CompletedTask;
    }

    public override Task OnActivated()
    {
        if (!Immutable)
        {
            Publisher.Publish(Notify.As(Factory.Create<CommandCollection>(new List<IDisposable>
            {
                Factory.Create<ConfirmItemActionViewModel>(),
                Factory.Create<DismissItemActionViewModel>(),
            })));
        }
        else if (Archived)
        {
            Publisher.Publish(Notify.As(Factory.Create<CommandCollection>(new List<IDisposable>
            {
                Factory.Create<UnarchiveItemActionViewModel>(),
            })));
        }
        else
        {
            Publisher.Publish(Notify.As(Factory.Create<CommandCollection>(new List<IDisposable>
            {
                Factory.Create<FavouriteItemActionViewModel>(Favourite),
                Factory.Create<EditItemActionViewModel>(),
                Factory.Create<ArchiveItemActionViewModel>(),
            })));
        }

        return base.OnActivated();
    }
}
