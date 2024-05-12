using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class OpenContainerHandler(ContainerConfiguration configuration,
    ISecurityKeyFactory keyVaultFactory,
    IContainer vaultStorage) :
    IHandler<Open<Vault>, bool>
{
    public async Task<bool> Handle(Open<Vault> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is Vault vault && configuration.Name is { Length: > 0 } name && vault.Password is { Length: > 0 } password)
        {
            if (configuration.Key?.Split(':') is { Length: >= 2 } keyPart)
            {
                byte[]? salt = Convert.FromBase64String(keyPart[0]);
                byte[]? encryptedKey = Convert.FromBase64String(keyPart[1]);

                if ( keyVaultFactory.Create(Encoding.UTF8.GetBytes(password), encryptedKey, salt) is SecurityKey key)
                {
                    if (await vaultStorage.Create(name, key))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}