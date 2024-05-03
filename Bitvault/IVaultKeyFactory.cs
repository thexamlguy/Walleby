namespace Bitvault;

public interface IVaultKeyFactory
{
    VaultKey? Create(byte[] phrase,
        byte[]? encryptedKey = null,
        byte[]? salt = null);
}
