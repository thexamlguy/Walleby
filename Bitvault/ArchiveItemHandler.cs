using Toolkit.Foundation;

namespace Bitvault;

public class ArchiveItemHandler(IValueStore<Item> valueStore,
    ICache<Item> cache,
    IMediator mediator) :
    INotificationHandler<ArchiveEventArgs<Item>>
{
    public async Task Handle(ArchiveEventArgs<Item> args)
    {
        try
        {
            if (valueStore.Value is Item item)
            {
                if (cache.Contains(item))
                {
                    await mediator.Handle<UpdateEventArgs<(Guid, int)>,
                        bool>(new UpdateEventArgs<(Guid, int)>((item.Id, 2)));

                    cache.Remove(item);
                }
            } 
        }
        catch
        {

        }
    }
}