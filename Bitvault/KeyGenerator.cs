using System.Security.Cryptography;
namespace Bitvault;

public class KeyGenerator : 
    IKeyGenerator
{
    public byte[] Generate(int size)
    {
        byte[] key = new byte[size];
        RandomNumberGenerator.Fill(key);

        return key;
    }
}
