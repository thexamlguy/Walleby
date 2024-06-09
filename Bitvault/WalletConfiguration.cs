using Toolkit.Foundation;

namespace Bitvault;

public record WalletConfiguration :
    ComponentConfiguration
{
    public string? Key { get; set; }
}