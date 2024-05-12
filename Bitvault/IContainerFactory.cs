using Toolkit.Foundation;

namespace Bitvault
{
    public interface IContainerFactory
    {
        IComponentHost? Create(string name);
    }
}