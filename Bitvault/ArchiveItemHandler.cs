using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class ArchiveItemHandler(IValueStore<Item> valueStore,
    IDbContextFactory<ContainerDbContext> dbContextFactory) :
    INotificationHandler<ArchiveEventArgs<Item>>
{
    public async Task Handle(ArchiveEventArgs<Item> args)
    {
        try
        {
            if (valueStore.Value is Item item)
            {
                await Task.Run(async () =>
                {
                    using ContainerDbContext context = await dbContextFactory.CreateDbContextAsync();

                    if (await context.FindAsync<ItemEntry>(item.Id) is ItemEntry result)
                    {
                        result.State = 3;
                        await context.SaveChangesAsync();
                    }
                });
            }
        }
        catch
        {

        }
    }
}