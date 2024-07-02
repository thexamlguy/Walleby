using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template,
    NamedComponent named,
    Guid id,
    string name = "",
    string description = "",
    string category = "",
    bool isSelected = false,
    bool isFavourite = false,
    bool isArchived = false) :
    Observable(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<ArchiveEventArgs<Item>>,
    INotificationHandler<UnarchiveEventArgs<Item>>,
    INotificationHandler<FavouriteEventArgs<Item>>,
    INotificationHandler<UnfavouriteEventArgs<Item>>,
    INotificationHandler<DeleteEventArgs<Item>>,
    INotificationHandler<NotifyEventArgs<ItemHeader<string>>>,
    INotificationHandler<NotifyEventArgs<Item<IImageDescriptor>>>,
    IKeyed<Guid>,
    ISelectable,
    IRemovable
{
    [ObservableProperty]
    private string? category = category;

    [ObservableProperty]
    private string? description = description;

    [ObservableProperty]
    private Guid id = id;

    [ObservableProperty]
    private IImageDescriptor? imageDescriptor;

    [ObservableProperty]
    private bool isArchived = isArchived;
    [ObservableProperty]
    private bool isAttached;

    [ObservableProperty]
    private bool isFavourite = isFavourite;
    [ObservableProperty]
    private bool isSelected = isSelected;

    [ObservableProperty]
    private string? name = name;

    [ObservableProperty]
    private string named = $"{named}";
    public IContentTemplate Template { get; set; } = template;

    public Task Handle(ArchiveEventArgs<Item> args)
    {
        Dispose();
        return Task.CompletedTask;
    }

    public Task Handle(UnarchiveEventArgs<Item> args)
    {
        Dispose();
        return Task.CompletedTask;
    }

    public Task Handle(FavouriteEventArgs<Item> args)
    {
        IsFavourite = true;
        return Task.CompletedTask;
    }

    public Task Handle(UnfavouriteEventArgs<Item> args)
    {
        IsFavourite = false;
        return Task.CompletedTask;
    }

    public Task Handle(NotifyEventArgs<ItemHeader<string>> args)
    {
        if (args.Sender is ItemHeader<string> header)
        {
            Name = header.Value;
        }

        return Task.CompletedTask;
    }

    public Task Handle(DeleteEventArgs<Item> args)
    {
        Dispose();
        return Task.CompletedTask;
    }

    public Task Handle(NotifyEventArgs<Item<IImageDescriptor>> args)
    {
        if (args.Sender is Item<IImageDescriptor> item)
        {
            ImageDescriptor = item.Value;
        }

        return Task.CompletedTask;
    }

    [RelayCommand]
    private void Attached()
    {
        if (!IsAttached)
        {
            Publisher.Publish(Activation.As<ItemNavigationViewModel, Guid>(Id));
            IsAttached = true;
        }
    }
}