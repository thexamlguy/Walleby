using Toolkit.Foundation;

namespace Bitvault;

public class FavouriteItemHandler(IValueStore<Item> valueStore,
    IMediator mediator) :
    INotificationHandler<FavouriteEventArgs<Item>>
{
    public async Task Handle(FavouriteEventArgs<Item> args)
    {
        try
        {
            if (valueStore.Value is Item item)
            {
                await mediator.Handle<UpdateEventArgs<(Guid, int)>, bool>(new UpdateEventArgs<(Guid,
                    int)>((item.Id, 1)));
            }
        }
        catch
        {
        }
    }
}