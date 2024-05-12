using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateItemHandler(IDbContextFactory<ContainerDbContext> dbContextFactory, IPublisher publisher) :
    IHandler<CreateEventArgs<ItemConfiguration>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<ItemConfiguration> args,
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

                await publisher.Publish(Activated.As(configuration));
                return true;
            }
            catch
            {

            }
        }

        return false;
    }
}
