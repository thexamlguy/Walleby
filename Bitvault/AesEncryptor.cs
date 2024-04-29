using System.Security.Cryptography;

namespace Bitvault;

public class AesEncryptor : IEncryptor
{
    public string Encrypt(string plainText, byte[] key)
    {
        const int IvSize = 16;

        using var aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();

        byte[] iv = aes.IV;

        using var memoryStream = new MemoryStream();
        memoryStream.Write(iv, 0, IvSize); // Store IV at the start of the stream

        using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(plainText);
        }

        return Convert.ToBase64String(memoryStream.ToArray()); // Return the encrypted data in base64
    }
}
