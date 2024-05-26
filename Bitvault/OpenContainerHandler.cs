using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class OpenContainerHandler(ContainerConfiguration configuration,
    ISecurityKeyFactory keyVaultFactory,
    IContainerStorageFactory vaultStorage) :
    IHandler<ActivateEventArgs<Container>, bool>
{
    public async Task<bool> Handle(ActivateEventArgs<Container> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is Container container && configuration.Name is { Length: > 0 } name && container.Password is { Length: > 0 } password)
        {
            if (configuration.Key?.Split(':') is { Length: >= 2 } keyPart)
            {
                byte[]? salt = Convert.FromBase64String(keyPart[0]);
                byte[]? encryptedKey = Convert.FromBase64String(keyPart[1]);

                if (keyVaultFactory.Create(Encoding.UTF8.GetBytes(password), encryptedKey, salt) is SecurityKey key)
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