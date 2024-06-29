using Toolkit.Foundation;

namespace Wallet;

public interface IWalletHostFactory
{
    IComponentHost? Create(string key);
}