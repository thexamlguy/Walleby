namespace Bitvault;

public record QueryLockerConfiguration
{
    public string? Filter { get; set; }

    public string? Query { get; set; }
}