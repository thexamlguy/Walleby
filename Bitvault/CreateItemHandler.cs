using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateItemHandler(IDbContextFactory<LockerContext> dbContextFactory) :
    IHandler<CreateEventArgs<(Guid, string, string, ItemConfiguration)>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<(Guid, string, string, ItemConfiguration)> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is (Guid id, string name, string category, ItemConfiguration configuration))
        {
            try
            {
                using LockerContext context = dbContextFactory.CreateDbContext();
                EntityEntry<ItemEntry>? result = null;

                await Task.Run(async () =>
                {
                    result = await context.AddAsync(new ItemEntry { Id = id, Name = name, Category = category }, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                }, cancellationToken);

                if (result is not null)
                {
                    return true;
                }
            }
            catch
            {
            }
        }

        return false;
    }
}