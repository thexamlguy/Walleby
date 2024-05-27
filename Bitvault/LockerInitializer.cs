using Toolkit.Foundation;

namespace Bitvault;

public class LockerInitializer(IEnumerable<IConfigurationDescriptor<LockerConfiguration>> configurations,
    IComponentFactory componentFactory,
    ILockerHostCollection lockers) : IInitializer
{
    public async Task Initialize()
    {
        foreach (IConfigurationDescriptor<LockerConfiguration> configuration in configurations)
        {
            if (componentFactory.Create<ILockerComponent,
                LockerConfiguration>(configuration.Section, configuration.Value)
                is IComponentHost host)
            {
                lockers.Add(host);
                await host.StartAsync();
            }
        }
    }
}