using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateContainerHandler(IContainerComponentFactory componentFactory,
    IPublisher publisher) :
    IHandler<Create<Container>, bool>
{
    public async Task<bool> Handle(Create<Container> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is Container container && container.Name is { Length: > 0 } name &&
            container.Password is { Length: > 0 } password)
        {
            if (componentFactory.Create(name) is IComponentHost host)
            {
                ISecurityKeyFactory keyVaultFactory = host.Services.GetRequiredService<ISecurityKeyFactory>();
                IValueStore<SecurityKey> secureKeyStore = host.Services.GetRequiredService<IValueStore<SecurityKey>>();
                IContainerFactory containerFactory = host.Services.GetRequiredService<IContainerFactory>();

                if (keyVaultFactory.Create(Encoding.UTF8.GetBytes(password)) is SecurityKey key)
                {
                    secureKeyStore.Set(key);

                    if (await containerFactory.Create(name, key))
                    {
                        IWritableConfiguration<ContainerConfiguration> configuration =
                            host.Services.GetRequiredService<IWritableConfiguration<ContainerConfiguration>>();

                        configuration.Write(args => args.Key = $"{Convert.ToBase64String(key.Salt)}:{Convert.ToBase64String(key.EncryptedKey)}:{Convert.ToBase64String(key.DecryptedKey)}");
                        host.Start();

                        await publisher.Publish(Activated.As(host), cancellationToken);
                        return true;
                    }
                }
            }
        }

        return false;
    }
}