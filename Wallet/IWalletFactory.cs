using Toolkit.Foundation;

namespace Wallet
{
    public interface IWalletFactory
    {
        IComponentHost? Create(string key);
    }
}