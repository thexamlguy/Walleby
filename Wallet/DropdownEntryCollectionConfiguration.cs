using System.Text.Json.Serialization;

namespace Wallet;

public record DropdownEntryCollectionConfiguration :
    ItemEntryConfiguration<object>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IList<string> Values { get; set; } = new List<string>();
}
