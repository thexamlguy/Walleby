using Toolkit.Foundation;

namespace Bitvault;

public class FavouriteItemHandler(IValueStore<Item<(Guid, string)>> valueStore,
    IMediator mediator) :
    INotificationHandler<FavouriteEventArgs<Item>>
{
    public async Task Handle(FavouriteEventArgs<Item> args)
    {
        try
        {
            if (valueStore.Value is Item<(Guid, string)> item)
            {
                (Guid id, string name) = item.Value;
                await mediator.Handle<UpdateEventArgs<(Guid, int)>, bool>(new UpdateEventArgs<(Guid, int)>((id, 1)));
            }
        }
        catch
        {
        }
    }
}