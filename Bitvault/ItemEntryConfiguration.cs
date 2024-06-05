using System.Text.Json.Serialization;

namespace Bitvault;

public interface IItemEntryConfiguration
{
    string? Label { get; set; }

    object? Value { get; set; }
}

[JsonDerivedType(typeof(DropdownEntryConfiguration), typeDiscriminator: "Dropdown")]
[JsonDerivedType(typeof(MaskedTextEntryConfiguration), typeDiscriminator: "MaskedText")]
[JsonDerivedType(typeof(NoteEntryConfiguration), typeDiscriminator: "Note")]
[JsonDerivedType(typeof(NumberEntryConfiguration), typeDiscriminator: "Number")]
[JsonDerivedType(typeof(PasswordEntryConfiguration), typeDiscriminator: "Password")]
[JsonDerivedType(typeof(TextEntryConfiguration), typeDiscriminator: "Text")]
public record ItemEntryConfiguration : IItemEntryConfiguration
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Label { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Value { get; set; }
}