using System.Collections.ObjectModel;

namespace Bitvault;

public class WalletCommandHeaderCollection(IList<IDisposable> list) :
    ReadOnlyCollection<IDisposable>(list);