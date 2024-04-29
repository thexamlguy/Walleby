namespace Bitvault
{
    public interface IVaultConnectionPersistence
    {
        void Dispose();
        string? Get(string key);
        void Set(string key, string connection);
    }
}