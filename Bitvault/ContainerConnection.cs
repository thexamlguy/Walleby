namespace Bitvault;

public class ContainerConnection(string connection)
{
    private readonly string connection = connection;

    public override string ToString() => connection;
}
