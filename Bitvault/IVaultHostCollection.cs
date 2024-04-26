using Toolkit.Foundation;

namespace Bitvault;

public interface IVaultHostCollection :
    IEnumerable<IComponentHost>
{
    void Add(IComponentHost host);
}