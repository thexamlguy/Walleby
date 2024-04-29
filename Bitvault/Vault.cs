using System.Diagnostics.CodeAnalysis;

namespace Bitvault;

public record Vault
{
    public Vault(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public Vault(string password)
    {
        Password = password;
    }


    public Vault()
    {

    }

    [MaybeNull]
    public string Name { get; }

    [MaybeNull]
    public string? Password { get; }
}
