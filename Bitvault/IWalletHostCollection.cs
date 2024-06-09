using Toolkit.Foundation;

namespace Bitvault;

public interface IWalletHostCollection :
    IEnumerable<IComponentHost>
{
    void Add(IComponentHost host);
}