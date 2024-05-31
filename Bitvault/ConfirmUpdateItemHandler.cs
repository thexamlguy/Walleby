using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmUpdateItemHandler(IValueStore<Item<(Guid, string)>> store,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        ItemHeaderConfiguration? configuration = await mediator.Handle<ConfirmEventArgs<Item>,
            ItemHeaderConfiguration>(args);

        if (configuration is not null)
        {
            publisher.Publish(Notify.As(configuration));

            if (store?.Value is Item<(Guid, string)> item)
            {
                (Guid id, string _) = item.Value;
                string? name = configuration.Name;

                Item<(Guid, string)> newItem = new((id, name));
                publisher.Publish(Modified.As(item, newItem));

                store.Set(newItem);

                await mediator.Handle<UpdateEventArgs<(Guid, string, ItemConfiguration)>, bool>(new UpdateEventArgs<(Guid, string,
                    ItemConfiguration)>((id, name, new ItemConfiguration())));
            }
        }
    }
}