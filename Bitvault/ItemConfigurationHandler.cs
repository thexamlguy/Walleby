using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class ItemConfigurationHandler(IDbContextFactory<ContainerDbContext> dbContextFactory) :
    IHandler<Create<ItemConfiguration>, bool>
{
    public async Task<bool> Handle(Create<ItemConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is ItemConfiguration configuration)
        {
            try
            {
                await Task.Run(async () =>
                {
                    using ContainerDbContext context = dbContextFactory.CreateDbContext();
                    await context.AddAsync(new Data.Item { Name = configuration.Name }, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                }, cancellationToken);

                return true;
            }
            catch
            {

            }
        }

        return false;
    }
}
