using Toolkit.Foundation;

namespace Bitvault;

public interface ILockerHostCollection :
    IEnumerable<IComponentHost>
{
    void Add(IComponentHost host);
}