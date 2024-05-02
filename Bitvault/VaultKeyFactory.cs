using Toolkit.Foundation;

namespace Bitvault;

public class VaultKeyFactory(IKeyGenerator generator,
    IKeyDeriver deriver,
    IEncryptor encryptor,
    IDecryptor decryptor) : 
    IVaultKeyFactory
{
    public VaultKey Create(byte[] phrase,
        byte[]? encryptedKey = null,
        byte[]? salt = null)
    {
        salt ??= generator.Generate(16);
        byte[] derivedKey = deriver.DeriveKey(phrase, salt);

        encryptedKey ??= encryptor.Encrypt(generator.Generate(32), derivedKey);
        byte[] decryptedKey = decryptor.Decrypt(encryptedKey, derivedKey);

        return new VaultKey(salt, encryptedKey, decryptedKey);
    }
}

