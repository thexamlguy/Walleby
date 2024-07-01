using Toolkit.Foundation;

namespace Wallet;

public record ItemHeaderConfiguration
{
    public string? Name { get; set; }

    public string? Category { get; set; }

    public IImageDescriptor? ImageDescriptor { get; set; }
}