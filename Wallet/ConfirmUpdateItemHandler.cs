using Toolkit.Foundation;

namespace Wallet;

public class ConfirmUpdateItemHandler(IDecoratorService<Item<(Guid, string)>> itemDecorator,
    IDecoratorService<ItemConfiguration> itemConfigurationDecorator,
    IDecoratorService<ItemHeaderConfiguration> itemHeaderConfiguration,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        if (itemDecorator?.Value is Item<(Guid, string)> item &&
            itemHeaderConfiguration.Value is ItemHeaderConfiguration headerConfiguration &&
            itemConfigurationDecorator.Value is ItemConfiguration itemConfiguration)
        {
            if (headerConfiguration?.Name is { Length: > 0 } name &&
                headerConfiguration.Category is { Length: > 0 } category)
            {
                IImageDescriptor? imageDescriptor = headerConfiguration.ImageDescriptor;

                publisher.Publish(Notify.As(new Item<string>(name)));
                publisher.Publish(Notify.As(new Item<IImageDescriptor?>(imageDescriptor)));

                (Guid id, _) = item.Value;

                await mediator.Handle<UpdateEventArgs<Item<(Guid, string, string, IImageDescriptor?, 
                    ItemConfiguration)>>, bool>(new UpdateEventArgs<Item<(Guid, string, string, IImageDescriptor?,
                    ItemConfiguration)>>(new Item<(Guid, string, string, IImageDescriptor?, ItemConfiguration)>((id, 
                    name, category, imageDescriptor, itemConfiguration))));

                Item<(Guid, string)> newItem = new((id, name));
                publisher.Publish(Modified.As(item, newItem));

                itemDecorator.Set(newItem);
                publisher.Publish(Changed.As(newItem));
            }
        }
    }
}