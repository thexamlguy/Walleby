namespace Bitvault;

public record Unfavourite
{
    public static UnfavouriteEventArgs<TValue> As<TValue>(TValue value) =>
        new(value);

    public static UnfavouriteEventArgs<TValue> As<TValue>() where TValue : new() =>
        new(new TValue());
}