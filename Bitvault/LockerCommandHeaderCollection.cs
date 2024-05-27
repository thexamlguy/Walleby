using System.Collections.ObjectModel;

namespace Bitvault;

public class LockerCommandHeaderCollection(IList<IDisposable> list) :
    ReadOnlyCollection<IDisposable>(list);