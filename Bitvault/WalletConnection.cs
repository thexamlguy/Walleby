namespace Bitvault;

public class WalletConnection(string connection)
{
    private readonly string connection = connection;

    public override string ToString() => connection;
}