namespace Wallet;

public record PinEntryConfiguration :
    ItemEntryConfiguration
{
    public int Minimum { get; set; }

    public int Maximum { get; set; }
}
