using Toolkit.Foundation;

namespace Bitvault;
public class UnfavouriteItemHandler(IValueStore<Item> valueStore,
    IMediator mediator) :
    INotificationHandler<UnfavouriteEventArgs<Item>>
{
    public async Task Handle(UnfavouriteEventArgs<Item> args)
    {
        try
        {
            if (valueStore.Value is Item item)
            {
                await mediator.Handle<UpdateEventArgs<(Guid, int)>, bool>(new UpdateEventArgs<(Guid,
                    int)>((item.Id, 0)));
            }
        }
        catch
        {
        }
    }
}