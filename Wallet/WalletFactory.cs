﻿using Microsoft.Extensions.Hosting;
using System.Text;
using Toolkit.Foundation;

namespace Wallet;

public class WalletFactory(ISecurityKeyFactory securityKeyFactory,
    IWalletDatabaseFactory walletDatabaseFactory,
    IWritableConfiguration<WalletConfiguration> configuration,
    IHostEnvironment environment,
    IImageWriter imageWriter) :
    IWalletFactory
{
    public async Task<bool> Create(string name,
        string password,
        IImageDescriptor? imageDescriptor)
    {
        if (securityKeyFactory.Create(Encoding.UTF8.GetBytes(password)) is SecurityKey key)
        {
            if (await walletDatabaseFactory.Create(name, Convert.ToBase64String(key.DecryptedKey)))
            {
                configuration.Write(args => args.Key = $"{Convert.ToBase64String(key.Salt)}:" +
                    $"{Convert.ToBase64String(key.EncryptedKey)}:{Convert.ToBase64String(key.DecryptedKey)}");

                if (imageDescriptor is not null)
                {
                    string file = Path.Combine(environment.ContentRootPath, "Thumbnail.png");
                    using FileStream stream = File.OpenWrite(file);

                    imageWriter.Write(imageDescriptor, stream);
                }

                return true;
            }
        }

        return false;
    }
}