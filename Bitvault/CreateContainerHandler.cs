using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateContainerHandler(IContainerFactory componentFactory,
    IPublisher publisher) :
    IHandler<CreateEventArgs<ContainerToken>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<ContainerToken> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is ContainerToken container && container.Name is { Length: > 0 } name &&
            container.Password is { Length: > 0 } password)
        {
            if (componentFactory.Create(name) is IComponentHost host)
            {
                ISecurityKeyFactory keyVaultFactory = host.Services.GetRequiredService<ISecurityKeyFactory>();
                IValueStore<SecurityKey> secureKeyStore = host.Services.GetRequiredService<IValueStore<SecurityKey>>();
                IContainerStorageFactory containerFactory = host.Services.GetRequiredService<IContainerStorageFactory>();

                if (keyVaultFactory.Create(Encoding.UTF8.GetBytes(password)) is SecurityKey key)
                {
                    secureKeyStore.Set(key);

                    if (await containerFactory.Create(name, key))
                    {
                        IWritableConfiguration<ContainerConfiguration> configuration =
                            host.Services.GetRequiredService<IWritableConfiguration<ContainerConfiguration>>();

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