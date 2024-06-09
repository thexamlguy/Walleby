using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Bitvault;

public class WalletStorageFactory(IDecoratorService<WalletConnection> connection,
    IHostEnvironment environment,
    IServiceProvider provider) :
    IWalletStorageFactory
{
    public async Task<bool> Create(string name,
        SecurityKey key)
    {
        connection.Set(new WalletConnection($"Data Source={Path.Combine(environment.ContentRootPath, name)}" +
            $".vault;Mode=ReadWriteCreate;Pooling=true;Password={Convert.ToBase64String(key.DecryptedKey)}"));

        IDbContextFactory<WalletContext> dbContextFactory = provider.GetRequiredService<IDbContextFactory<WalletContext>>();
        using WalletContext context = await dbContextFactory.CreateDbContextAsync();

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