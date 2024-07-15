namespace Wallet;

public record NumberEntryConfiguration : 
    ItemEntryConfiguration<string>
{
    public int MinLength { get; set; }

    public int MaxLength { get; set; }
}