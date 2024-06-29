using Toolkit.Foundation;

namespace Wallet;

public class WalletFactory(IComponentFactory componentFactory) :
    IWalletFactory
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