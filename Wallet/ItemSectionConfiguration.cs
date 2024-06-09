namespace Wallet;

public record ItemSectionConfiguration
{
    public IList<ItemEntryConfiguration> Entries { get; set; } = new List<ItemEntryConfiguration>();
}