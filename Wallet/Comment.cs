namespace Wallet;

public record Comment
{
    public DateTimeOffset Created { get; set; }

    public string? Text { get; set; }
}