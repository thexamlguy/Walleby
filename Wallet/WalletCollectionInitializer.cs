using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Wallet;

public class WalletCollectionInitializer(IHostEnvironment environment, 
    IComponentFactory componentFactory,
    IWalletHostCollection Wallets) : 
    IInitialization
{
    public void Initialize()
    {
        foreach (string wallet in Directory.EnumerateDirectories(Path.Combine(environment.ContentRootPath, "Wallet")))
        {
            string name = Path.GetFileName(wallet);
            string section = $"Wallet:{name}";

            if (componentFactory.Create<WalletComponent,
                WalletConfiguration>(section, new WalletConfiguration())
                is IComponentHost host)
            {
                Wallets.Add(host);
                host.Start();
            }
        }
    }
}