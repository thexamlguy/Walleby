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
            bool success = false;
            if (valueStore?.Value is Item item)
            {
                (bool Success, int Id, string Name) = await mediator.Handle<EditEventArgs<(int, ItemConfiguration)>,
                    (bool, int, string)>(new EditEventArgs<(int, ItemConfiguration)>((item.Id, new ItemConfiguration { Name = configuration.Name })));

                if (Success)
                {
                    Item newItem = new() { Id = Id, Name = Name };
                    publisher.Publish(Modified.As(item, newItem));

                    valueStore.Set(newItem);
                    success = true;
                }
            }
            else
            {
                (bool Success, int Id, string Name) = await mediator.Handle<CreateEventArgs<ItemConfiguration>,
                    (bool, int, string)>(new CreateEventArgs<ItemConfiguration>(new ItemConfiguration { Name = configuration.Name }));

                if (Success)
                {
                    publisher.Publish(Created.As(new Item { Id = Id, Name = Name }));
                    success = true;
                }
            }

            if (success)
            {
                publisher.Publish(Notify.As(configuration));
            }
        }
    }
}
