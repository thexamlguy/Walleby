using Toolkit.Foundation;

namespace Bitvault;

public class VaultCollectionInitializer(IEnumerable<IConfigurationDescriptor<VaultConfiguration>> configurations,
    IComponentFactory componentFactory, 
    IVaultHostCollection vaults) : IInitializer
{
    public async Task Initialize()
    {
        foreach (IConfigurationDescriptor<VaultConfiguration> configuration in configurations)
        {       
            if (componentFactory.Create<IVaultComponent, VaultConfiguration>(configuration.Section, configuration.Value) is IComponentHost host)
            {
                vaults.Add(host);
                await host.StartAsync();
            }

        }
    }
}
