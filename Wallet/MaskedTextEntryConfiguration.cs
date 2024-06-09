using System.Text.Json.Serialization;

namespace Wallet;

public record MaskedTextEntryConfiguration :
    ItemEntryConfiguration
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Pattern { get; set; }
}
