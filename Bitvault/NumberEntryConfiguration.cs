namespace Bitvault;

public record NumberEntryConfiguration : 
    ItemEntryConfiguration
{
    public int MinLength { get; set; }

    public int MaxLength { get; set; }
}