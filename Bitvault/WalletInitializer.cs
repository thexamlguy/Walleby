using Toolkit.Foundation;

namespace Bitvault;

public class WalletInitializer(IEnumerable<IConfigurationDescriptor<WalletConfiguration>> configurations,
    IComponentFactory componentFactory,
    IWalletHostCollection Wallets) : IInitializer
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