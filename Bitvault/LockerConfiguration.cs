using Toolkit.Foundation;

namespace Bitvault;

public record LockerConfiguration :
    ComponentConfiguration
{
    public string? Name { get; set; }

    public string? Key { get; set; }
}