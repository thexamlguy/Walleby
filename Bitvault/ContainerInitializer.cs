using Toolkit.Foundation;

namespace Bitvault;

public class ContainerInitializer(IEnumerable<IConfigurationDescriptor<ContainerConfiguration>> configurations,
    IComponentFactory componentFactory, 
    IContainerHostCollection vaults) : IInitializer
{
    public async Task Initialize()
    {
        foreach (IConfigurationDescriptor<ContainerConfiguration> configuration in configurations)
        {       
            if (componentFactory.Create<IContainerComponent,
                ContainerConfiguration>(configuration.Section, configuration.Value) 
                is IComponentHost host)
            {
                vaults.Add(host);
                await host.StartAsync();
            }
        }
    }
}
