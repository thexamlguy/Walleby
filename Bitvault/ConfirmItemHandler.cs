using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmItemHandler(IValueStore<Item<(Guid, string)>> valueStore,
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

            if (valueStore?.Value is Item<(Guid, string)> item)
            {
                (Guid id, string _) = item.Value;

                string? name = configuration.Name;

                Item<(Guid, string)> newItem = new((id, name));
                publisher.Publish(Modified.As(item, newItem));

                valueStore.Set(newItem);

                await mediator.Handle<UpdateEventArgs<(Guid, string, ItemConfiguration)>, bool>(new UpdateEventArgs<(Guid, string,
                    ItemConfiguration)>((id, name, new ItemConfiguration())));
            }
            else
            {
                Guid id = Guid.NewGuid();
                string? name = configuration.Name;

                bool Success = await mediator.Handle<CreateEventArgs<(Guid, string,
                    ItemConfiguration)>, bool>(new CreateEventArgs<(Guid, string, ItemConfiguration)>((id, name, new ItemConfiguration())));

                if (Success)
                {
                    publisher.Publish(Created.As(new Item<(Guid, string)>((id, name))));
                }
            }
        }
    }
}