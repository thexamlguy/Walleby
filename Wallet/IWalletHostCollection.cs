using Toolkit.Foundation;

namespace Wallet;

public interface IWalletHostCollection :
    IEnumerable<IComponentHost>
{
    void Add(IComponentHost host);
}