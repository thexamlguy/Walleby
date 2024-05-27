namespace Bitvault;

public class LockerConnection(string connection)
{
    private readonly string connection = connection;

    public override string ToString() => connection;
}