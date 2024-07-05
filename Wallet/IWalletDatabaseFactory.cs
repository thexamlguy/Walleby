namespace Wallet;

public interface IWalletDatabaseFactory
{
    Task<bool> Create(string name, string key);
}