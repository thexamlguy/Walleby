using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class UnarchiveItemHandler(IValueStore<Item> valueStore,
    IDbContextFactory<ContainerDbContext> dbContextFactory) :
    INotificationHandler<UnarchiveEventArgs<Item>>
{
    public async Task Handle(UnarchiveEventArgs<Item> args)
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
                        result.State = 0;
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