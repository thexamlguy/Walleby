using Toolkit.Foundation;

namespace Bitvault;

public class ContainerComponentFactory(IComponentFactory componentFactory) : 
    IContainerComponentFactory
{
    public IComponentHost? Create(string name)
    {
        if (componentFactory.Create<IContainerComponent, ContainerConfiguration>($"Vault:{name}", 
            new ContainerConfiguration { Name = name }) is IComponentHost host)
        {
            return host;
        }

        return default;
    }
}
