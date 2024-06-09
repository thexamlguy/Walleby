using Toolkit.Foundation;

namespace Bitvault
{
    public interface IWalletFactory
    {
        IComponentHost? Create(string name);
    }
}