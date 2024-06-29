using Toolkit.Foundation;

namespace Wallet;

public interface IWalletFactory
{
    Task<bool> Create(string name, 
        string password,
        IImageDescriptor thumbnail);
}
