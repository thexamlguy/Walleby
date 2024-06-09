using Toolkit.Foundation;

namespace Wallet;

public class WalletFactory(IComponentFactory componentFactory) :
    IWalletFactory
{
    public IComponentHost? Create(string name)
    {
        if (componentFactory.Create<IWalletComponent, WalletConfiguration>($"Wallet:{name}", new WalletConfiguration()) is IComponentHost host)
        {
            return host;
        }

        return default;
    }
}