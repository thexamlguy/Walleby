namespace Bitvault;

public interface IEncryptor
{

    byte[] Encrypt(byte[] data, byte[] key);
}