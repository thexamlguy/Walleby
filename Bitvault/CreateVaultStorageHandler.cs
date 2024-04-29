using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateVaultStorageHandler(IHostEnvironment environment,
    IKeyDeriver deriver,
    IEncryptor encryptor,
    IDecryptor decryptor,
    IDbContextFactory<VaultDbContext> dbContextFactory) : IHandler<Create<VaultStorage>, bool>
{
    public async Task<bool> Handle(Create<VaultStorage> args, CancellationToken cancellationToken)
    {
        if (args.Value is VaultStorage vault)
        {
            if (vault.Name is { Length: > 0 } name && vault.Password is { Length: > 0 } password)
            {
                byte[] salt = new byte[16];
                RandomNumberGenerator.Fill(salt);

                byte[] key = new byte[32];
                RandomNumberGenerator.Fill(key);

                byte[] derivedKey = deriver.DeriveKey(password, salt);
                string? encryptedKey = encryptor.Encrypt(Convert.ToBase64String(key), derivedKey);


                byte[] derivedKey2 = deriver.DeriveKey(password, salt);
                var dod = decryptor.Decrypt(encryptedKey, derivedKey2);

                // Derive the key for encryption

                //byte[] encryptionKey = deriver.DeriveKey(password, salt);

                //// Derive the key for decryption
                //byte[] decryptionKey = deriver.DeriveKey(password, salt);

                //// Compare keys to ensure they're the same
                //bool areKeysEqual = encryptionKey.SequenceEqual(decryptionKey);

                ////byte[] derivedKey = deriver.DeriveKey(password, salt);
                //string? encrypted = encryptor.Encrypt(password, derivedKey);

                //var storedSalt = Convert.ToBase64String(salt);


                //byte[] derivedKey2 = deriver.DeriveKey(password, salt);

                //var d = decryptor.Decrypt(encrypted, derivedKey2);

                // Generate a hash
                //string hash = hasher.HashPassword(password);

                //string[] parts = hash.Split(':');

                //// Store the salt only
                //string storedSalt = parts[0]; 

                //// Use the hash as the password
                //string storedHash = parts[1];

                //context.Database.SetConnectionString($"Data Source={Path.Combine(environment.ContentRootPath, name)}.vault;Mode=ReadWriteCreate;Password={storedHash}");
                //await context.Database.EnsureCreatedAsync();

                return true;
            }
        }

        return false;
    }
}