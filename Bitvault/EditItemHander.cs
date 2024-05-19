using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class EditItemHander(IDbContextFactory<ContainerDbContext> dbContextFactory) :
    IHandler<EditEventArgs<(int, ItemConfiguration)>, bool>
{
    public async Task<bool> Handle(EditEventArgs<(int, ItemConfiguration)> args,
        CancellationToken cancellationToken)
    {
        //if (args.Value is ItemConfiguration configuration)
        //{
        //    try
        //    {
        //        using ContainerDbContext context = dbContextFactory.CreateDbContext();
        //        EntityEntry<ItemEntry>? result = null;

        //        await Task.Run(async () =>
        //        {
        //            result = await context.AddAsync(new ItemEntry { Name = configuration.Name }, cancellationToken);
        //            await context.SaveChangesAsync(cancellationToken);

        //        }, cancellationToken);

        //        if (result is not null)
        //        {
        //            return true;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        return false;
    }
}
