using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class OpenVaultHandler(VaultConfiguration configuration,
    IVaultKeyFactory keyVaultFactory,
    IVaultStorage vaultStorage) :
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

                VaultKey key = keyVaultFactory.Create(Encoding.UTF8.GetBytes(password), encryptedKey, salt);
                if (await vaultStorage.CreateAsync(name, key))
                {

                }
            }
        }

        return false;
    }
}