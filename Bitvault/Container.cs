namespace Bitvault;

public record Container
{
    public Container(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public Container(string password)
    {
        Password = password;
    }

    public Container()
    {
    }

    public string Name { get; } = "";

    public string? Password { get; } = "";
}