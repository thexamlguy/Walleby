namespace Bitvault;

public record VaultKey(byte[] Phrase, byte[] Public, byte[] Private) :
    IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Array.Clear(Phrase, 0, Phrase.Length);
        Array.Clear(Public, 0, Public.Length);
        Array.Clear(Private, 0, Private.Length);
    }
}