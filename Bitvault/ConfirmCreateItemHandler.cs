using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmCreateItemHandler(IMediator mediator,
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

            Guid id = Guid.NewGuid();

            string? name = configuration.Name;
            string? category = configuration.Name;

            publisher.Publish(Created.As(new Item<(Guid, string)>((id, name))));

            await mediator.Handle<CreateEventArgs<(Guid, string, string,
                ItemConfiguration)>, bool>(new CreateEventArgs<(Guid, string, string, ItemConfiguration)>((id, name, category,
                    new ItemConfiguration())));
        }
    }
}
