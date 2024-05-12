namespace Bitvault;

public record Item<TValue>(TValue? Value = default);

public record Item
{
    public Item(int id)
    {
        Id = id;
    }

    public Item()
    {

    }

    public static Item<TValue> As<TValue>(TValue value) => new(value);

    public static Item<TValue> As<TValue>() where TValue : new() => new(new TValue());

    public int Id { get; }
}

