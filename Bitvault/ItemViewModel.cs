using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemViewModel : 
    ObservableCollection<IDisposable>,
    INotificationHandler<EditEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>
{
    [ObservableProperty]
    private bool archived;

    public ItemViewModel(IServiceProvider provider, 
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher, 
        ISubscription subscriber, 
        IDisposer disposer,
        IContentTemplate template,
        bool favourite = false,
        bool archived = false) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Archived = archived;

        if (!Archived)
        {
            Publisher.Publish(Notify.As(Factory.Create<CommandCollection>(new List<IDisposable>
            {
                Factory.Create<FavouriteItemActionViewModel>(favourite),
                Factory.Create<EditItemActionViewModel>(),
                Factory.Create<ArchiveItemActionViewModel>(),
            })));
        }
        else
        {
            Publisher.Publish(Notify.As(Factory.Create<CommandCollection>(new List<IDisposable>
            {
                Factory.Create<UnarchiveItemActionViewModel>(),
            })));
        }
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
}
