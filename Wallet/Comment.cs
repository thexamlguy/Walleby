namespace Wallet;

public record Comment
{
    public DateTimeOffset DateTime { get; set; }

    public string? Text { get; set; }
}
