using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Bitvault;

public class VaultStorageHandler(IHostEnvironment environment,
    IDbContextFactory<VaultDbContext> dbContextFactory) :
    IHandler<Create<VaultStorage>, bool>
{
    public async Task<bool> Handle(Create<VaultStorage> args, CancellationToken cancellationToken)
    {
        if (args.Value is VaultStorage storage)
        {
            using VaultDbContext context = dbContextFactory.CreateDbContext();
            await Task.Run(async () =>
            {
                context.Database.SetConnectionString($"Data Source={Path.Combine(environment.ContentRootPath, storage.Name)}.vault;Mode=ReadWriteCreate;Password={storage.Password}");
                await context.Database.EnsureCreatedAsync(cancellationToken);
            }, cancellationToken);

            return true;
        }

        return false;
    }
}
