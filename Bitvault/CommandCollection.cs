using System.Collections.ObjectModel;

namespace Bitvault;

public class CommandCollection(IList<IDisposable> list) :
    ReadOnlyCollection<IDisposable>(list)
{
}