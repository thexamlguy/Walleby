using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Wallet;

public class WalletCollectionInitializer(IHostEnvironment environment, 
    IComponentFactory componentFactory,
    IWalletHostCollection wallets) : 
    IInitialization
{
    public void Initialize()
    {
        string path = Path.Combine(environment.ContentRootPath, "Wallet");
        if (!Directory.Exists(path))
        {
            return;
        }

        foreach (string wallet in Directory.EnumerateDirectories(path))
        {
            string name = Path.GetFileName(wallet);
            string section = $"Wallet:{name}";

            if (componentFactory.Create<WalletComponent,
                WalletConfiguration>(section, new WalletConfiguration())
                is IComponentHost host)
            {
                wallets.Add(host);
                host.Start();
            }
        }
    }
}