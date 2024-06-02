namespace Bitvault;

public interface IItemConfigurationCollection :
    IReadOnlyDictionary<string, Func<ItemConfiguration>>;
