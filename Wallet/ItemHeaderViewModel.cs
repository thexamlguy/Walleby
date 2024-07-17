using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class ItemHeaderViewModel :
    Observable<string>,
    IHandler<ValidateEventArgs<ItemEntry>, bool>,
    INotificationHandler<UpdateEventArgs<ItemEntry>>,
    INotificationHandler<ConfirmEventArgs<ItemEntry>>,
    INotificationHandler<CancelEventArgs<ItemEntry>>,
    INotificationHandler<NotifyEventArgs<ItemCategory<string>>>,
    INotificationHandler<NotifyEventArgs<Item<IImageDescriptor>>>,
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
        IValidation validation,
        ItemHeaderConfiguration configuration,
        ItemState state,
        string value,
        IImageDescriptor? imageDescriptor = null) : base(provider, factory, mediator, publisher, subscriber, disposer, value)
    {
        Validation = validation;

        this.configuration = configuration;

        State = state;
        Value = value;
        ImageDescriptor = imageDescriptor;

        Track(nameof(Value), () => Value, x => Value = x);
        Track(nameof(ImageDescriptor), () => ImageDescriptor, x => ImageDescriptor = x);

        Validation.Add(() => Value, [new ValidationRule(() => Value is { Length: > 0 }, "Name is required")],
            ValidationTrigger.Deferred);
    }

    [ObservableProperty]
    private IValidation validation;

    public Task Handle(UpdateEventArgs<ItemEntry> args) =>
        Task.FromResult(State = ItemState.Write);

    public Task Handle(CancelEventArgs<ItemEntry> args)
    {
        Revert();

        State = ItemState.Read;
        return Task.CompletedTask;
    }

    public Task Handle(ConfirmEventArgs<ItemEntry> args)
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

    public Task Handle(NotifyEventArgs<Item<IImageDescriptor>> args)
    {
        if (args.Sender is Item<IImageDescriptor> item)
        {
            ImageDescriptor = item.Value;
        }

        return Task.CompletedTask;
    }

    public async Task<bool> Handle(ValidateEventArgs<ItemEntry> args,
        CancellationToken cancellationToken) => await Validation.Validate();

    protected override void OnValueChanged()
    {
        if (configuration is not null)
        {
            configuration.Name = Value;
        }
    }

    [RelayCommand]
    private async Task Import() => ImageDescriptor = await Mediator.Handle<CreateEventArgs<ProfileImage>,
        IImageDescriptor>(Create.As<ProfileImage>());

    private partial void OnImageDescriptorChanged(IImageDescriptor? value)
    {
        if (configuration is not null)
        {
            configuration.ImageDescriptor = value;
        }
    }

    private partial void OnImageDescriptorChanged(IImageDescriptor? oldValue,
        IImageDescriptor? newValue)
    {
        if (configuration is not null)
        {
            if (State is ItemState.Write)
            {
                if (newValue != oldValue)
                {
                    configuration.ImageDescriptor = newValue;
                }
            }
            else
            {
                configuration.ImageDescriptor = newValue;
            }
        }
    }

    [RelayCommand]
    private void Remove() => ImageDescriptor = null;
}