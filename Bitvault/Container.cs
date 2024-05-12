using System.Diagnostics.CodeAnalysis;

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


    [MaybeNull]
    public string Name { get; }

    [MaybeNull]
    public string? Password { get; }
}
