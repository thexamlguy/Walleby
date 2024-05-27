using System.Text.Json.Serialization;

namespace Bitvault;

public record MaskedTextEntryConfiguration :
    ItemEntryConfiguration
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Pattern { get; set; }
}
