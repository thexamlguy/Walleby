using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateVaultHandler(IVaultComponentFactory componentFactory) :
    IHandler<Create<Vault>, bool>
{
    public async Task<bool> Handle(Create<Vault> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is Vault vault && vault.Name is { Length: > 0 } name &&
            vault.Password is { Length: > 0 } password)
        {
            if (componentFactory.Create(name) is IComponentHost host)
            {
                IVaultKeyFactory keyVaultFactory = host.Services.GetRequiredService<IVaultKeyFactory>();
                IContainer<VaultKey> vaultKeyContainer = host.Services.GetRequiredService<IContainer<VaultKey>>();
                IVaultStorage vaultStorage = host.Services.GetRequiredService<IVaultStorage>();

                if (keyVaultFactory.Create(Encoding.UTF8.GetBytes(password)) is VaultKey key)
                {
                    vaultKeyContainer.Set(key);

                    if (await vaultStorage.Create(name, key))
                    {
                        IWritableConfiguration<VaultConfiguration> configuration =
                            host.Services.GetRequiredService<IWritableConfiguration<VaultConfiguration>>();

                        configuration.Write(args => args.Key = $"{Convert.ToBase64String(key.Salt)}:{Convert.ToBase64String(key.EncryptedKey)}:{Convert.ToBase64String(key.DecryptedKey)}");
                        host.Start();

                        return true;
                    }
                }
            }
        }

        return false;
    }
}