using Toolkit.Foundation;

namespace Bitvault;

public record LockerConfiguration :
    ComponentConfiguration
{
    public string? Key { get; set; }
}