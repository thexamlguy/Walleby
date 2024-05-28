using Toolkit.Foundation;

namespace Bitvault;

public class LockerFactory(IComponentFactory componentFactory) :
    ILockerFactory
{
    public IComponentHost? Create(string name)
    {
        if (componentFactory.Create<ILockerComponent, LockerConfiguration>($"Locker:{name}", new LockerConfiguration()) is IComponentHost host)
        {
            return host;
        }

        return default;
    }
}