namespace Wallet;

public interface IWalletStoreFactory
{
    Task<bool> Create(string name, SecurityKey key);
}