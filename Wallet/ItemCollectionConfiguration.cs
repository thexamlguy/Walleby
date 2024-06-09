namespace Wallet;

public record ItemCollectionConfiguration
{
    public string? Filter { get; set; } = "All";

    public string? Query { get; set; }
}