using Toolkit.Foundation;

namespace Bitvault;
public class UnfavouriteItemHandler(IValueStore<Item<(Guid, string)>> valueStore,
    IMediator mediator) :
    INotificationHandler<UnfavouriteEventArgs<Item<(Guid, string)>>>
{
    public async Task Handle(UnfavouriteEventArgs<Item<(Guid, string)>> args)
    {
        try
        {
            if (valueStore.Value is Item<(Guid, string)> item)
            {
                (Guid id, string name) = item.Value;
                await mediator.Handle<UpdateEventArgs<(Guid, int)>, bool>(new UpdateEventArgs<(Guid, int)>((id, 0)));
            }
        }
        catch
        {
        }
    }
}