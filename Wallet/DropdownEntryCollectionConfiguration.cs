using System.Text.Json.Serialization;

namespace Wallet;

public record DropdownEntryCollectionConfiguration :
    ItemEntryConfiguration
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<string> Values { get; set; } = new List<string>();
}
