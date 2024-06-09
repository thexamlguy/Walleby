namespace Wallet;

public interface IItemConfigurationCollection :
    IReadOnlyDictionary<string, Func<ItemConfiguration>>;
