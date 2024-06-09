using Toolkit.Foundation;

namespace Wallet;

public class ConfirmUpdateItemHandler(IDecoratorService<Item<(Guid, string)>> itemDecorator,
    IDecoratorService<ItemConfiguration> itemConfigurationDecorator,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        if (itemDecorator?.Service is Item<(Guid, string)> item && 
            itemConfigurationDecorator.Service is ItemConfiguration configuration)
        {
            string? name = await mediator.Handle<ConfirmEventArgs<ItemHeader>,
                string>(Confirm.As<ItemHeader>());

            if (name is not null)
            {
                publisher.Publish(Notify.As(new ItemHeader<string>(name)));

                (Guid id, string _) = item.Value;

                Item<(Guid, string)> newItem = new((id, name));
                publisher.Publish(Modified.As(item, newItem));

                itemDecorator.Set(newItem);

                await mediator.Handle<UpdateEventArgs<Item<(Guid, string, ItemConfiguration)>>, bool>(new UpdateEventArgs<Item<(Guid, string,
                    ItemConfiguration)>>(new Item<(Guid, string, ItemConfiguration)>((id, name, configuration))));
            }
        }
    }
}