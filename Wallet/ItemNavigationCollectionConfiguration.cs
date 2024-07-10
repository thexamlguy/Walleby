namespace Wallet;

public record ItemNavigationCollectionConfiguration
{
    public string? Filter { get; set; } = "All";

    public string? Query { get; set; }
}