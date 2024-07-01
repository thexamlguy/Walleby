using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemHeaderViewModel : 
    Observable<string, string>,
    INotificationHandler<UpdateEventArgs<Item>>,
    INotificationHandler<ConfirmEventArgs<Item>>,
    INotificationHandler<CancelEventArgs<Item>>,
    INotificationHandler<NotifyEventArgs<ItemCategory<string>>>,
    IItemViewModel
{
    private readonly ItemHeaderConfiguration configuration;

    [ObservableProperty]
    private string? category;

    [ObservableProperty]
    private IImageDescriptor? imageDescriptor;
    [ObservableProperty]
    private ItemState state;

    public ItemHeaderViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        ItemHeaderConfiguration configuration,
        ItemState state,
        string key,
        string value) : base(provider, factory, mediator, publisher, subscriber, disposer, key, value)
    {
        this.configuration = configuration;

        State = state;
        Value = value;

        Track(nameof(Value), () => Value, newValue => Value = newValue);
    }

    public Task Handle(UpdateEventArgs<Item> args) =>
        Task.FromResult(State = ItemState.Write);

    public Task Handle(CancelEventArgs<Item> args)
    {
        Revert();

        State = ItemState.Read;
        return Task.CompletedTask;
    }

    public Task Handle(ConfirmEventArgs<Item> args)
    {
        Commit();

        State = ItemState.Read;
        return Task.CompletedTask;
    }

    public Task Handle(NotifyEventArgs<ItemCategory<string>> args)
    {
        if (args.Sender is ItemCategory<string> category)
        {
            Category = category.Value;
            configuration.Category = category.Value;
        }

        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task Import() => ImageDescriptor = await Mediator.Handle<CreateEventArgs<ProfileImage>,
        IImageDescriptor>(Create.As<ProfileImage>());

    [RelayCommand]
    public void Remove() => ImageDescriptor = null;

    protected override void OnValueChanged()
    {
        if (configuration is not null)
        {
            configuration.Name = Value;
        }
    }
}