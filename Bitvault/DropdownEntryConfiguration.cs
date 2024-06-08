using System.Text.Json.Serialization;

namespace Bitvault;

public record DropdownEntryConfiguration :
    ItemEntryConfiguration
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<string> Values { get; set; } = new List<string>();
}
