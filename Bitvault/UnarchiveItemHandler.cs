﻿using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class UnarchiveItemHandler(IValueStore<Item<(Guid, string)>> valueStore,
    IDbContextFactory<LockerContext> dbContextFactory) :
    INotificationHandler<UnarchiveEventArgs<Item>>
{
    public async Task Handle(UnarchiveEventArgs<Item> args)
    {
        try
        {
            if (valueStore.Value is Item<(Guid, string)> item)
            {
                (Guid id, string name) = item.Value;
                await Task.Run(async () =>
                {
                    using LockerContext context = await dbContextFactory.CreateDbContextAsync();

                    if (await context.FindAsync<ItemEntry>(id) is ItemEntry result)
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