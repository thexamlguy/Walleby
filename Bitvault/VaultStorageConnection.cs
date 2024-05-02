namespace Bitvault;

public class VaultStorageConnection(string connection)
{
    private readonly string connection = connection;

    public override string ToString() => connection;
}
