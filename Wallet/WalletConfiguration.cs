using Toolkit.Foundation;

namespace Wallet;

public record WalletConfiguration :
    ComponentConfiguration
{
    public string? Key { get; set; }
}