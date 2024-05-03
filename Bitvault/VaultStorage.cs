using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Bitvault;

public class VaultStorage(IContainer<VaultStorageConnection> connection,
    IHostEnvironment environment,
    IServiceProvider provider) :
    IVaultStorage
{
    public async Task<bool> Create(string name, 
        VaultKey key)
    {
        connection.Set(new VaultStorageConnection($"Data Source={Path.Combine(environment.ContentRootPath, name)}" +
            $".vault;Mode=ReadWriteCreate;Pooling=false;Password={Convert.ToBase64String(key.DecryptedKey)}"));

        IDbContextFactory<VaultDbContext> dbContextFactory = provider.GetRequiredService<IDbContextFactory<VaultDbContext>>();
        using VaultDbContext context = await dbContextFactory.CreateDbContextAsync();

        try
        {
            await Task.Run(async () =>
            {
                await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
                await context.Database.CloseConnectionAsync().ConfigureAwait(false);

                context.Database.SetConnectionString(null);

            }).ConfigureAwait(false);
        }
        catch
        {
            return false;
        }

        return true;
    }
}
