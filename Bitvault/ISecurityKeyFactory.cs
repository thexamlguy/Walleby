namespace Bitvault;

public interface ISecurityKeyFactory
{
    SecurityKey? Create(byte[] phrase,
        byte[]? encryptedKey = null,
        byte[]? salt = null);
}
