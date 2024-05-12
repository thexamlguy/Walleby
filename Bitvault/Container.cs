using System.Diagnostics.CodeAnalysis;

namespace Bitvault;

public record Container<TValue>(TValue? Value = default);

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


    public static Container<TValue> As<TValue>(TValue value) => new(value);

    public static Container<TValue> As<TValue>() where TValue : new() => new(new TValue());


    [MaybeNull]
    public string Name { get; }

    [MaybeNull]
    public string? Password { get; }
}
