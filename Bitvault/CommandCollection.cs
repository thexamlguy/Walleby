using System.Collections.ObjectModel;

namespace Bitvault;

public class CommandCollection :
    ReadOnlyCollection<IDisposable>
{
    public CommandCollection(IList<IDisposable> list) : base(list)
    {

    }
}
