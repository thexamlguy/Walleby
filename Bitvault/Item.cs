namespace Bitvault;

public record Item
{
    public Guid Id { get; init; }

    public string? Name { get; init; } = "";
}

