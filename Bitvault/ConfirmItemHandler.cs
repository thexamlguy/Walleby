using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmItemHandler(IValueStore<Item> valueStore,
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

            if (valueStore?.Value is Item item)
            {
                Guid id = item.Id;
                string? name = configuration.Name;

                Item newItem = new() { Id = id, Name = name };
                publisher.Publish(Modified.As(item, newItem));

                valueStore.Set(newItem);

                await mediator.Handle<EditEventArgs<(Guid, ItemConfiguration)>, bool>(new EditEventArgs<(Guid,
                    ItemConfiguration)>((item.Id, new ItemConfiguration { Name = name })));
            }
            else
            {
                Guid id = Guid.NewGuid();
                string? name = configuration.Name;

                bool Success = await mediator.Handle<CreateEventArgs<(Guid,
                    ItemConfiguration)>, bool>(new CreateEventArgs<(Guid, ItemConfiguration)>((id, new ItemConfiguration { Name = name })));

                if (Success)
                {
                    publisher.Publish(Created.As(new Item { Id = id, Name = name }));
                }
            }
        }
    }
}