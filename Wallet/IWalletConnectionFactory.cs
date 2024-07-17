namespace Wallet
{
    public interface IWalletConnectionFactory
    {
        Task<WalletConnection?> Create(string name, string key);
    }
}