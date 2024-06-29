using Toolkit.Foundation;

namespace Wallet;

public class WalletHostFactory(IComponentFactory componentFactory) :
    IWalletHostFactory
{
    public IComponentHost? Create(string key)
    {
        if (componentFactory.Create<WalletComponent, WalletConfiguration>($"Wallet:{key}",
            new WalletConfiguration()) is IComponentHost host)
        {
            return host;
        }

        return default;
    }
}