using System.Text;
using Toolkit.Foundation;

namespace Wallet;

public class OpenWalletHandler(IConfigurationDescriptor<WalletConfiguration> descriptor,
    ISecurityKeyFactory securityKeyFactory,
    IWalletConnectionFactory walletConnectionFactory,
    IDecoratorService<WalletConnection> walletConnectionDecorator) :
    IHandler<OpenEventArgs<Wallet<string>>, bool>
{
    public async Task<bool> Handle(OpenEventArgs<Wallet<string>> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is Wallet<string> Wallet &&
            descriptor.Name is { Length: > 0 } name &&
            Wallet.Value is { Length: > 0 } password)
        {
            WalletConfiguration configuration = descriptor.Value;
            if (configuration.Key?.Split(':') is { Length: >= 2 } keyPart)
            {
                byte[]? salt = Convert.FromBase64String(keyPart[0]);
                byte[]? encryptedKey = Convert.FromBase64String(keyPart[1]);

                if (securityKeyFactory.Create(Encoding.UTF8.GetBytes(password),
                    encryptedKey, salt) is SecurityKey securityKey)
                {
                    if (await walletConnectionFactory.Create(name, Convert.ToBase64String(securityKey.DecryptedKey))
                        is WalletConnection connection)
                    {
                        walletConnectionDecorator.Set(connection);
                        return true;
                    }
                }
            }
        }

        return false;
    }
}