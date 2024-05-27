namespace Bitvault;

public record Locker
{
    public Locker(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public Locker(string password)
    {
        Password = password;
    }

    public Locker()
    {
    }

    public string Name { get; } = "";

    public string? Password { get; } = "";
}