namespace Bitvault;

public record ContainerToken
{
    public ContainerToken(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public ContainerToken(string password)
    {
        Password = password;
    }

    public ContainerToken()
    {
    }

    public string Name { get; } = "";

    public string? Password { get; } = "";
}