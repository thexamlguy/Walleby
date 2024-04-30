namespace Bitvault;

public interface IDecryptor
{
    byte[] Decrypt(byte[] cipher,
        byte[] key);
}