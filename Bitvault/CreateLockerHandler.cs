using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateLockerHandler(ILockerFactory componentFactory,
    IPublisher publisher) :
    IHandler<CreateEventArgs<Locker>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<Locker> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is Locker locker && locker.Name is { Length: > 0 } name &&
            locker.Password is { Length: > 0 } password)
        {
            if (componentFactory.Create(name) is IComponentHost host)
            {
                ISecurityKeyFactory keyVaultFactory = host.Services.GetRequiredService<ISecurityKeyFactory>();
                IDecoratorService<SecurityKey> secureKeyStore = host.Services.GetRequiredService<IDecoratorService<SecurityKey>>();
                ILockerStorageFactory lockerStorageFactory = host.Services.GetRequiredService<ILockerStorageFactory>();

                if (keyVaultFactory.Create(Encoding.UTF8.GetBytes(password)) is SecurityKey key)
                {
                    secureKeyStore.Set(key);

                    if (await lockerStorageFactory.Create(name, key))
                    {
                        IWritableConfiguration<LockerConfiguration> configuration =
                            host.Services.GetRequiredService<IWritableConfiguration<LockerConfiguration>>();

                        configuration.Write(args => args.Key = $"{Convert.ToBase64String(key.Salt)}:{Convert.ToBase64String(key.EncryptedKey)}:{Convert.ToBase64String(key.DecryptedKey)}");
                        host.Start();

                        publisher.Publish(Activated.As(host), cancellationToken);
                        return true;
                    }
                }
            }
        }

        return false;
    }
}