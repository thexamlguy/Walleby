using System.Diagnostics.CodeAnalysis;

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


    [MaybeNull]
    public string Name { get; }

    [MaybeNull]
    public string? Password { get; }
}
