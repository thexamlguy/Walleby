namespace Wallet;

public record ItemSectionConfiguration
{
    public IList<IItemEntryConfiguration> Entries { get; set; } = new List<IItemEntryConfiguration>();
}