using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Bitvault;

public class ContainerFactory(IContainer<ContainerConnection> connection,
    IHostEnvironment environment,
    IServiceProvider provider) :
    IContainer
{
    public async Task<bool> Create(string name, 
        SecurityKey key)
    {
        connection.Set(new ContainerConnection($"Data Source={Path.Combine(environment.ContentRootPath, name)}" +
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
