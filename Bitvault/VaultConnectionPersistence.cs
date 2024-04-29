namespace Bitvault;

public class VaultConnectionPersistence :
    IVaultConnectionPersistence,
    IDisposable
{
    private string? connection;

    public void Dispose()
    {
        connection = null;
    }

    public string? Get(string key)
    {
        return connection;
    }

    public void Set(string key,
        string connection)
    {
        this.connection = connection;
    }
}
