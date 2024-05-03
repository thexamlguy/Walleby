using Toolkit.Foundation;

namespace Bitvault;

public class VaultKeyFactory(IKeyGenerator generator,
    IKeyDeriver deriver,
    IEncryptor encryptor,
    IDecryptor decryptor) : 
    IVaultKeyFactory
{
    public VaultKey? Create(byte[] phrase,
        byte[]? encryptedKey = null,
        byte[]? salt = null)
    {
        salt ??= generator.Generate(16);
        byte[] derivedKey = deriver.DeriveKey(phrase, salt);

        if (encryptedKey is null)
        {
            if (!encryptor.TryEncrypt(generator.Generate(32), derivedKey, out encryptedKey) || encryptedKey is null)
            {
                return default;
            }
        }

        if (!decryptor.TryDecrypt(encryptedKey, derivedKey, out byte[]? decryptedKey) || decryptedKey is null)
        {
            return default;
        }

        return new VaultKey(salt, encryptedKey, decryptedKey);
    }
}

