using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class OpenWalletHandler(IConfigurationDescriptor<WalletConfiguration> descriptor,
    ISecurityKeyFactory securityKeyFactory,
    IWalletStorageFactory WalletStorageFactory) :
    IHandler<ActivateEventArgs<Wallet<string>>, bool>
{
    public async Task<bool> Handle(ActivateEventArgs<Wallet<string>> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is Wallet<string> Wallet && 
            descriptor.Name is { Length: > 0 } name &&
            Wallet.Value is { Length: > 0 } password)
        {
            WalletConfiguration configuration = descriptor.Value;
            if (configuration.Key?.Split(':') is { Length: >= 2 } keyPart)
            {
                byte[]? salt = Convert.FromBase64String(keyPart[0]);
                byte[]? encryptedKey = Convert.FromBase64String(keyPart[1]);

                if (securityKeyFactory.Create(Encoding.UTF8.GetBytes(password), encryptedKey, salt) is SecurityKey key)
                {
                    if (await WalletStorageFactory.Create(name, key))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}