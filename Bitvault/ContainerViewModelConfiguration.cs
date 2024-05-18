namespace Bitvault;

public record ContainerViewModelConfiguration
{
    public string? Filter { get; set; } = "All";

    public string? Query { get; set; }
}
