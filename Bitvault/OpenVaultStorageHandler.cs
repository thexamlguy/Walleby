using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Bitvault;

public class OpenVaultStorageHandler(IHostEnvironment environment,
    IDbContextFactory<VaultDbContext> dbContextFactory) : IHandler<Open<VaultStorage>, bool>
{
    public async Task<bool> Handle(Open<VaultStorage> args, CancellationToken cancellationToken)
    {
        if (args.Value is VaultStorage vault)
        {
            if (vault.Name is { Length: > 0 } name && vault.Password is { Length: > 0 } password)
            {
                using VaultDbContext context = dbContextFactory.CreateDbContext();
                var d = context.Database.GetDbConnection().ConnectionString;
                context.Database.SetConnectionString($"Data Source={Path.Combine(environment.ContentRootPath, name)}.vault;Mode=ReadWriteCreate;Password={password}");

                bool isOpen = false;
                await Task.Run(async () =>
                {
                    try
                    {
                        await context.Database.OpenConnectionAsync();
                        isOpen = true;
                    }
                    catch
                    {
                        // We are ignoring this exception as it is either a go, or not.
                    }

                }, cancellationToken);

                return isOpen;
            }
        }

        return false;
    }
}
