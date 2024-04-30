using Toolkit.Foundation;

namespace Bitvault
{
    public interface IVaultComponentFactory
    {
        IComponentHost? Create(string name);
    }
}