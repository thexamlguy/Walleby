using System.Collections;
using Toolkit.Foundation;

namespace Bitvault;

public class LockerHostCollection :
   ILockerHostCollection

{
    private readonly List<IComponentHost> hosts = [];

    public void Add(IComponentHost host) => hosts.Add(host);

    public IEnumerator<IComponentHost> GetEnumerator() =>
        hosts.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        hosts.GetEnumerator();
}