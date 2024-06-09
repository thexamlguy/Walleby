using System.Collections.ObjectModel;

namespace Wallet;

public class ItemConfigurationCollection(IDictionary<string, Func<ItemConfiguration>> dictionary) : 
    ReadOnlyDictionary<string, Func<ItemConfiguration>>(dictionary),
    IItemConfigurationCollection;
