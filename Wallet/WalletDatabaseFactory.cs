using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Wallet.Data;

namespace Wallet;

public class WalletDatabaseFactory(IHostEnvironment environment) :
    IWalletDatabaseFactory
{
    public async Task<bool> Create(string name, string key)
    {
        string databaseFile = $"{Path.Combine(environment.ContentRootPath, name)}.wallet";
        try
        {
            WalletConnection connection = new($"Data Source={databaseFile};Mode=ReadWriteCreate;Pooling=true;Password={key}");

            await Task.Run(async () =>
            {
                using WalletContext context = new(connection);
                await context.Database.EnsureCreatedAsync();

                context.Database.GetDbConnection().Close();
                context.Database.SetConnectionString(null);
            });
        }
        catch
        {
            return false;
        }

        return true;
    }
}