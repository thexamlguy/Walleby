using Toolkit.Foundation;

namespace Bitvault;

public class VaultsInitializer(IEnumerable<IConfigurationDescriptor<VaultConfiguration>> configurations,
    IVaultFactory factory) : IInitializer
{
    public async Task Initialize()
    {
        foreach (IConfigurationDescriptor<VaultConfiguration> configuration in configurations)
        {
            //if (factory.Create(configuration.Section, configuration.Value) is IComponentHost host)
            //{
            //    await host.StartAsync();
            //}
        }
    }
}