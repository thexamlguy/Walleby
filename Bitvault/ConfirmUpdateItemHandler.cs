using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmUpdateItemHandler(IDecoratorService<Item<(Guid, string)>> decoratorItem,
    IDecoratorService<ItemConfiguration> decoratorItemConfiguration,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        string? name = await mediator.Handle<ConfirmEventArgs<ItemHeader>,
            string>(Confirm.As<ItemHeader>());

        if (name is not null)
        {
            var dd = decoratorItemConfiguration;
            publisher.Publish(Notify.As(new ItemHeader<string>(name)));
            if (decoratorItem?.Value is Item<(Guid, string)> item)
            {
                (Guid id, string _) = item.Value;

                Item<(Guid, string)> newItem = new((id, name));
                publisher.Publish(Modified.As(item, newItem));

                decoratorItem.Set(newItem);

                await mediator.Handle<UpdateEventArgs<(Guid, string, ItemConfiguration)>, bool>(new UpdateEventArgs<(Guid, string,
                    ItemConfiguration)>((id, name, new ItemConfiguration())));
            }
        }
    }
}