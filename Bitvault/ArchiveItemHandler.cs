using Toolkit.Foundation;

namespace Bitvault;

public class ArchiveItemHandler(IValueStore<Item<(Guid, string)>> valueStore,
    ICache<Item<(Guid, string)>> cache,
    IMediator mediator) :
    INotificationHandler<ArchiveEventArgs<Item>>
{
    public async Task Handle(ArchiveEventArgs<Item> args)
    {
        try
        {
            if (valueStore.Value is Item<(Guid, string)> item)
            {
                if (cache.Contains(item))
                {
                    (Guid id, string name) = item.Value;

                    await mediator.Handle<UpdateEventArgs<(Guid, int)>,
                        bool>(new UpdateEventArgs<(Guid, int)>((id, 2)));

                    cache.Remove(item);
                }
            } 
        }
        catch
        {

        }
    }
}