using System.Text.Json.Serialization;

namespace Bitvault;

public record DropdownEntryConfiguration :
    ItemEntryConfiguration
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Values { get; set; }
}
