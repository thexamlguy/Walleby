using Toolkit.Foundation;

namespace Bitvault;

public record ContainerConfiguration :
    ComponentConfiguration
{
    public string? Name { get; set; }

    public string? Key { get; set; }
}