using Wallet.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Toolkit.Foundation;

namespace Wallet;

public class UnarchiveItemHandler(IDecoratorService<Item<(Guid, string)>> decoratorService,
    IDbContextFactory<WalletContext> dbContextFactory) :
    INotificationHandler<UnarchiveEventArgs<Item>>
{
    public async Task Handle(UnarchiveEventArgs<Item> args)
    {
        try
        {
            if (decoratorService.Service is Item<(Guid, string)> item)
            {
                (Guid id, string name) = item.Value;

                using WalletContext context = await dbContextFactory.CreateDbContextAsync();
                if (await context.FindAsync<ItemEntry>(id) is ItemEntry result)
                {
                    result.State = 0;
                    await context.SaveChangesAsync();
                }
            }
        }
        catch
        {
        }
    }
}