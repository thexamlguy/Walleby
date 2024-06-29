using Microsoft.Extensions.Hosting;
using System.Text;
using Toolkit.Foundation;

namespace Wallet;

public class WalletFactory(ISecurityKeyFactory securityKeyFactory,
    IDecoratorService<SecurityKey> secureKeyStore,
    IWalletStoreFactory walletStoreFactory,
    IWritableConfiguration<WalletConfiguration> configuration,
    IHostEnvironment environment,
    IImageWriter imageWriter) :
    IWalletFactory
{
    public async Task<bool> Create(string name,
        string password, 
        IImageDescriptor thumbnail)
    {
        if (securityKeyFactory.Create(Encoding.UTF8.GetBytes(password)) is SecurityKey key)
        {
            secureKeyStore.Set(key);

            if (await walletStoreFactory.Create(name, key))
            {
                configuration.Write(args => args.Key = $"{Convert.ToBase64String(key.Salt)}:" +
                    $"{Convert.ToBase64String(key.EncryptedKey)}:{Convert.ToBase64String(key.DecryptedKey)}");

                string file = Path.Combine(environment.ContentRootPath, "Thumbnail.png");
                using FileStream stream = File.OpenWrite(file);

                imageWriter.Write(thumbnail, stream);
                return true;
            }
        }

        return false;
    }
}
