using Toolkit.Foundation;

namespace Bitvault;

public interface IContainerHostCollection :
    IEnumerable<IComponentHost>
{
    void Add(IComponentHost host);
}