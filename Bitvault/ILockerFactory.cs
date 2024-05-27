using Toolkit.Foundation;

namespace Bitvault
{
    public interface ILockerFactory
    {
        IComponentHost? Create(string name);
    }
}