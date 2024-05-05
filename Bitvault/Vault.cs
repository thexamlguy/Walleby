using System.Diagnostics.CodeAnalysis;

namespace Bitvault;

public record Vault<TValue>(TValue? Value = default);

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


    public static Vault<TValue> As<TValue>(TValue value) => new(value);

    public static Vault<TValue> As<TValue>() where TValue : new() => new(new TValue());


    [MaybeNull]
    public string Name { get; }

    [MaybeNull]
    public string? Password { get; }
}
