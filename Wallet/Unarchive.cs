namespace Wallet;

public record Unarchive
{
    public static UnarchiveEventArgs<TValue> As<TValue>(TValue value) =>
        new(value);

    public static UnarchiveEventArgs<TValue> As<TValue>() where TValue : new() =>
        new(new TValue());
}