using System.Security.Cryptography;

namespace Bitvault;

public class AesDecryptor :
    IDecryptor
{
    private const int IvSize = 16;

    public string Decrypt(string cipherText, byte[] key)
    {
        byte[] cipherData = Convert.FromBase64String(cipherText);

        byte[] iv = new byte[IvSize];
        Array.Copy(cipherData, 0, iv, 0, IvSize); // Extract the IV from the start of the cipher data

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var memoryStream = new MemoryStream(cipherData, IvSize, cipherData.Length - IvSize);
        using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
        using (var streamReader = new StreamReader(cryptoStream))
        {
            return streamReader.ReadToEnd(); // Return the decrypted text
        }
    }
}
