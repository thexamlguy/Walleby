using System.Collections.ObjectModel;

namespace Wallet;

public class ItemCommandHeaderCollection(IList<IDisposable> list) :
    ReadOnlyCollection<IDisposable>(list);