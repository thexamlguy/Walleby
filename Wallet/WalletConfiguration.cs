using Toolkit.Foundation;

namespace Wallet;

public record WalletConfiguration :
    ComponentConfiguration
{
    public string? Key { get; set; }

    public long? LockTimeout { get; set; } = 300000;
}