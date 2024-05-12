namespace Bitvault;

public record SecurityKey(byte[] Salt, byte[] EncryptedKey, byte[] DecryptedKey) :
    IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Array.Clear(Salt, 0, Salt.Length);
        Array.Clear(EncryptedKey, 0, EncryptedKey.Length);
        Array.Clear(DecryptedKey, 0, DecryptedKey.Length);
    }
}