using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmItemHandler(IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        ItemHeaderConfiguration? configuration = await mediator.Handle<ConfirmEventArgs<Item>,
            ItemHeaderConfiguration>(args);

        (bool Success, int Id, string Name) result = await mediator.Handle<CreateEventArgs<ItemConfiguration>,
            (bool, int, string)>(new CreateEventArgs<ItemConfiguration>(new ItemConfiguration { Name = configuration?.Name }));

        if (result.Success)
        {
            publisher.Publish(Activated.As(new Item { Id = result.Id, Name = result.Name }));
        }
    }
}
