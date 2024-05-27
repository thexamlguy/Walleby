using System.Text.Json.Serialization;

namespace Bitvault;

[JsonDerivedType(typeof(DropdownEntryConfiguration), typeDiscriminator: "Dropdown")]
[JsonDerivedType(typeof(MaskedTextEntryConfiguration), typeDiscriminator: "MaskedText")]
[JsonDerivedType(typeof(NoteEntryConfiguration), typeDiscriminator: "Note")]
[JsonDerivedType(typeof(NumberEntryConfiguration), typeDiscriminator: "Number")]
[JsonDerivedType(typeof(PasswordEntryConfiguration), typeDiscriminator: "Password")]
[JsonDerivedType(typeof(TextEntryConfiguration), typeDiscriminator: "Text")]
public record ItemEntryConfiguration
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Label { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Value { get; set; }
}