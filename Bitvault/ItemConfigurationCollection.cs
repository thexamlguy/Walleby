using System.Collections.ObjectModel;

namespace Bitvault;

public class ItemConfigurationCollection(IDictionary<string, Func<ItemConfiguration>> dictionary) : 
    ReadOnlyDictionary<string, Func<ItemConfiguration>>(dictionary),
    IItemConfigurationCollection;
