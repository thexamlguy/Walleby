using Toolkit.Foundation;

namespace Bitvault;

public class VaultKeyFactory(IKeyGenerator generator,
    IKeyDeriver deriver,
    IEncryptor encryptor,
    IDecryptor decryptor) : 
    IVaultKeyFactory
{
    public VaultKey Create(string password)
    {
        byte[] salt = generator.Generate(16);
        byte[] key = generator.Generate(32);

        byte[] derivedKey = deriver.DeriveKey(password, salt);

        byte[] publicKey = encryptor.Encrypt(key, derivedKey);
        byte[] privateKey = decryptor.Decrypt(publicKey, derivedKey);

        return new VaultKey(salt, publicKey, privateKey);
    }
}

