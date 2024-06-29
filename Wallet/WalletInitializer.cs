using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Wallet;

public class WalletInitializer(IHostEnvironment environment, 
    IComponentFactory componentFactory,
    IWalletHostCollection Wallets) : 
    IInitialization
{
    public async Task Initialize()
    {
        foreach (string wallet in Directory.EnumerateDirectories(Path.Combine(environment.ContentRootPath, "Wallet")))
        {
            string name = Path.GetFileName(wallet);
            string section = $"Wallet:{name}";

            if (componentFactory.Create<WalletComponent,
                WalletConfiguration>(section)
                is IComponentHost host)
            {
                Wallets.Add(host);
                await host.StartAsync();
            }
        }
    }
}