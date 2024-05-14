namespace Bitvault;

public record Archive
{
    public static ArchiveEventArgs<TValue> As<TValue>(TValue value) =>
        new(value);

    public static ArchiveEventArgs<TValue> As<TValue>() where TValue : new() =>
        new(new TValue());
}
