//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
//using Toolkit.Foundation;

//namespace Bitvault;

//public class CreateVaultStorageHandler(IHostEnvironment environment,
//    IKeyGenerator generator,
//    IKeyDeriver deriver,
//    IEncryptor encryptor,
//    IDecryptor decryptor,
//    IDbContextFactory<VaultDbContext> dbContextFactory,
//    IWritableConfiguration<VaultConfiguration> writer) : IHandler<Create<VaultStorage>, bool>
//{
//    public async Task<bool> Handle(Create<VaultStorage> args, CancellationToken cancellationToken)
//    {
//        if (args.Value is VaultStorage vault)
//        {
//            if (vault.Name is { Length: > 0 } name && vault.Password is { Length: > 0 } password)
//            {
//                byte[] salt = generator.Generate(16);
//                byte[] key = generator.Generate(32);

//                byte[] derivedKey = deriver.DeriveKey(password, salt);

//                byte[] encryptedKey = encryptor.Encrypt(key, derivedKey);
//                byte[] decryptedKey = decryptor.Decrypt(encryptedKey, derivedKey);

//                Array.Clear(encryptedKey, 0, encryptedKey.Length);

//                using VaultDbContext context = dbContextFactory.CreateDbContext();
//                context.Database.SetConnectionString($"Data Source={Path.Combine(environment.ContentRootPath, name)}.vault;Mode=ReadWriteCreate;Password={Convert.ToBase64String(decryptedKey)}");

//                try
//                {
//                    await context.Database.EnsureCreatedAsync(cancellationToken);
//                    writer.Write(args =>
//                    {
//                        var f = args.Name;
//                    });
//                }
//                catch
//                {

//                }

//                Array.Clear(decryptedKey, 0, decryptedKey.Length);

//                // Derive the key for encryption

//                //byte[] encryptionKey = deriver.DeriveKey(password, salt);

//                //// Derive the key for decryption
//                //byte[] decryptionKey = deriver.DeriveKey(password, salt);

//                //// Compare keys to ensure they're the same
//                //bool areKeysEqual = encryptionKey.SequenceEqual(decryptionKey);

//                ////byte[] derivedKey = deriver.DeriveKey(password, salt);
//                //string? encrypted = encryptor.Encrypt(password, derivedKey);

//                //var storedSalt = Convert.ToBase64String(salt);


//                //byte[] derivedKey2 = deriver.DeriveKey(password, salt);

//                //var d = decryptor.Decrypt(encrypted, derivedKey2);

//                // Generate a hash
//                //string hash = hasher.HashPassword(password);

//                //string[] parts = hash.Split(':');

//                //// Store the salt only
//                //string storedSalt = parts[0]; 

//                //// Use the hash as the password
//                //string storedHash = parts[1];

//                //context.Database.SetConnectionString($"Data Source={Path.Combine(environment.ContentRootPath, name)}.vault;Mode=ReadWriteCreate;Password={storedHash}");
//                //await context.Database.EnsureCreatedAsync();

//                return true;
//            }
//        }

//        return false;
//    }
//}