namespace Bitvault;

public record QueryContainerConfiguration
{
    public string? Filter { get; set; }

    public string? Query { get; set; }
}