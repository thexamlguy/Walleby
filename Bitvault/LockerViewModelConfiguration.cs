namespace Bitvault;

public record LockerViewModelConfiguration
{
    public string? Filter { get; set; } = "All";

    public string? Query { get; set; }
}