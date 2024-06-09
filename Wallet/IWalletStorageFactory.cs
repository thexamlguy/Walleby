namespace Wallet;

public interface IWalletStorageFactory
{
    Task<bool> Create(string name, SecurityKey key);
}