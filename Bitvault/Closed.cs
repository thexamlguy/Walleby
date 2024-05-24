using Toolkit.Foundation;

namespace Bitvault;

public record Closed
{
    public static ChangedEventArgs<TValue> As<TValue>(TValue value) => new(value);

    public static ChangedEventArgs<TValue> As<TValue>() where TValue : new() => new(new TValue());
}