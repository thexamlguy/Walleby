namespace Wallet;

public record Attachment
{
    public DateTimeOffset DateTime { get; set; }

    public string? Name { get; set; }

    public string? Path { get; set;  }
}