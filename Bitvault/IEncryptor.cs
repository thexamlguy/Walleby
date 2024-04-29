namespace Bitvault
{
    public interface IEncryptor
    {

        string? Encrypt(string plainText, byte[] key);
    }
}