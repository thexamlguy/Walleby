using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmItemHandler(IMediator mediator) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        (bool result, int index) result = await mediator.Handle<CreateEventArgs<ItemConfiguration>,
            (bool, int)>(new CreateEventArgs<ItemConfiguration>(new ItemConfiguration()));

        ItemHeaderConfiguration? configuration = await mediator.Handle<ConfirmEventArgs<Item>,
            ItemHeaderConfiguration>(args);
    }
}
