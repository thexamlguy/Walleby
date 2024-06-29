using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Wallet;

public class WalletInitializer(IHostingEnvironment environment, IEnumerable<IConfigurationDescriptor<WalletConfiguration>> configurations,
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