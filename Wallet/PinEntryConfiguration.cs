namespace Wallet;

public record PinEntryConfiguration :
    ItemEntryConfiguration<string>
{
    public int Minimum { get; set; }

    public int Maximum { get; set; }
}