using Toolkit.Foundation;

namespace Bitvault;

public class VaultConfigurationInitializer(IEnumerable<IConfigurationDescriptor<VaultConfiguration>> configurations,
    IVaultFactory factory) : IInitializer
{
    public async Task Initialize()
    {
        foreach (IConfigurationDescriptor<VaultConfiguration> configuration in configurations)
        {
            await factory.CreateAsync(configuration.Section, 
                configuration.Value);
        }
    }
}
