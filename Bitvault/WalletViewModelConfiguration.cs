namespace Bitvault;

public record WalletViewModelConfiguration
{
    public string? Filter { get; set; } = "All";

    public string? Query { get; set; }
}