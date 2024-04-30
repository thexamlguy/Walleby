using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Bitvault;

public class VaultStorage(IHostEnvironment environment,
    IDbContextFactory<VaultDbContext> dbContextFactory) : 
    IVaultStorage
{
    public bool Create(string name, VaultKey key)
    {
        using VaultDbContext context = dbContextFactory.CreateDbContext();
        context.Database.SetConnectionString($"Data Source={Path.Combine(environment.ContentRootPath, name)}" +
            $".vault;Mode=ReadWriteCreate;Password={Convert.ToBase64String(key.Private)}");

        return true;
    }
}
