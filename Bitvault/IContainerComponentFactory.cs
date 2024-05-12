using Toolkit.Foundation;

namespace Bitvault
{
    public interface IContainerComponentFactory
    {
        IComponentHost? Create(string name);
    }
}