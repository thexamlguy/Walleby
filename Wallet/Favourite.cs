namespace Wallet;

public record Favourite
{
    public static FavouriteEventArgs<TValue> As<TValue>(TValue value) =>
        new(value);

    public static FavouriteEventArgs<TValue> As<TValue>() where TValue : new() =>
        new(new TValue());
}