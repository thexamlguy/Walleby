using System.Collections.ObjectModel;

namespace Bitvault;

public class ItemCommandHeaderCollection(IList<IDisposable> list) :
    ReadOnlyCollection<IDisposable>(list);
