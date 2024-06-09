using System.Collections.ObjectModel;

namespace Wallet;

public class WalletCommandHeaderCollection(IList<IDisposable> list) :
    ReadOnlyCollection<IDisposable>(list);