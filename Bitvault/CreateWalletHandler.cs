using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateWalletHandler(IWalletFactory componentFactory,
    IPublisher publisher) :
    IHandler<CreateEventArgs<Wallet<(string, string)>>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<Wallet<(string, string)>> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is Wallet <(string, string)> Wallet)
        {
            if (Wallet.Value is (string name, string password) && 
                name is { Length: > 0 } &&
                password is { Length: > 0 })
            {
                if (componentFactory.Create(name) is IComponentHost host)
                {
                    ISecurityKeyFactory keyVaultFactory = host.Services.GetRequiredService<ISecurityKeyFactory>();
                    IDecoratorService<SecurityKey> secureKeyStore = host.Services.GetRequiredService<IDecoratorService<SecurityKey>>();
                    IWalletStorageFactory WalletStorageFactory = host.Services.GetRequiredService<IWalletStorageFactory>();

                    if (keyVaultFactory.Create(Encoding.UTF8.GetBytes(password)) is SecurityKey key)
                    {
                        secureKeyStore.Set(key);

                        if (await WalletStorageFactory.Create(name, key))
                        {
                            IWritableConfiguration<WalletConfiguration> configuration =
                                host.Services.GetRequiredService<IWritableConfiguration<WalletConfiguration>>();

                            configuration.Write(args => args.Key = $"{Convert.ToBase64String(key.Salt)}:{Convert.ToBase64String(key.EncryptedKey)}:{Convert.ToBase64String(key.DecryptedKey)}");
                            host.Start();

                            publisher.Publish(Activated.As(host), cancellationToken);
                            return true;
                        }
                    }
                }

            }
        }

        return false;
    }
}