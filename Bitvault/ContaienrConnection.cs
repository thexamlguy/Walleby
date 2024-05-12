namespace Bitvault;

public class ContaienrConnection(string connection)
{
    private readonly string connection = connection;

    public override string ToString() => connection;
}
