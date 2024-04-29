namespace Bitvault
{
    public interface IDecryptor
    {
        string? Decrypt(string cipherText, byte[] key);
    }
}