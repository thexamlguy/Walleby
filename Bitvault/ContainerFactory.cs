using Toolkit.Foundation;

namespace Bitvault;

public class ContainerFactory(IComponentFactory componentFactory) :
    IContainerFactory
{
    public IComponentHost? Create(string name)
    {
        if (componentFactory.Create<IContainerComponent, ContainerConfiguration>($"Locker:{name}",
            new ContainerConfiguration { Name = name }) is IComponentHost host)
        {
            return host;
        }

        return default;
    }
}