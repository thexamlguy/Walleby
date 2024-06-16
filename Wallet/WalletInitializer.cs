using Toolkit.Foundation;

namespace Wallet;

public class WalletInitializer(IEnumerable<IConfigurationDescriptor<WalletConfiguration>> configurations,
    IComponentFactory componentFactory,
    IWalletHostCollection Wallets) : 
    IInitialization
{
    public async Task Initialize()
    {
        foreach (IConfigurationDescriptor<WalletConfiguration> configuration in configurations)
        {
            if (componentFactory.Create<IWalletComponent,
                WalletConfiguration>(configuration.Section, configuration.Value)
                is IComponentHost host)
            {
                Wallets.Add(host);
                await host.StartAsync();
            }
        }
    }
}