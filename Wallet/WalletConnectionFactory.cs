using Wallet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Wallet;

public class WalletConnectionFactory(IHostEnvironment environment) :
    IWalletConnectionFactory
{
    public async Task<WalletConnection?> Create(string name, string key)
    {
        string databaseFile = $"{Path.Combine(environment.ContentRootPath, name)}.wallet";
        if (File.Exists(databaseFile))
        {
            try
            {
                return await Task.Run(async () =>
                {
                    WalletConnection connection = new($"Data Source={databaseFile};Mode=ReadWriteCreate;Pooling=true;Password={key}");

                    using WalletContext context = new(connection);
                    await context.Database.OpenConnectionAsync().ConfigureAwait(false);

                    return connection;
                });
            }
            catch
            {
                return null;
            }
        }

        return null;
    }
}
