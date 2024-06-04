using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmUpdateItemHandler(IDecoratorService<Item<(Guid, string)>> store,
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


            publisher.Publish(Notify.As(new ItemHeader<string>(name)));
            if (store?.Value is Item<(Guid, string)> item)
            {
                (Guid id, string _) = item.Value;

                Item<(Guid, string)> newItem = new((id, name));
                publisher.Publish(Modified.As(item, newItem));

                store.Set(newItem);

                await mediator.Handle<UpdateEventArgs<(Guid, string, ItemConfiguration)>, bool>(new UpdateEventArgs<(Guid, string,
                    ItemConfiguration)>((id, name, new ItemConfiguration())));
            }
        }
    }
}