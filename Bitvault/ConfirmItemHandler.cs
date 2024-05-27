using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmItemHandler(IValueStore<Item<(Guid, string)>> valueStore,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item<(Guid, string)>>>
{
    public async Task Handle(ConfirmEventArgs<Item<(Guid, string)>> args)
    {
        ItemHeaderConfiguration? configuration = await mediator.Handle<ConfirmEventArgs<Item<(Guid, string)>>,
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

                await mediator.Handle<UpdateEventArgs<(Guid, ItemConfiguration)>, bool>(new UpdateEventArgs<(Guid,
                    ItemConfiguration)>((id, new ItemConfiguration { Name = name })));
            }
            else
            {
                Guid id = Guid.NewGuid();
                string? name = configuration.Name;

                bool Success = await mediator.Handle<CreateEventArgs<(Guid,
                    ItemConfiguration)>, bool>(new CreateEventArgs<(Guid, ItemConfiguration)>((id, new ItemConfiguration { Name = name })));

                if (Success)
                {
                    publisher.Publish(Created.As(new Item<(Guid, string)>((id, name))));
                }
            }
        }
    }
}