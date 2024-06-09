using System.Text.Json.Serialization;

namespace Wallet;

[JsonDerivedType(typeof(DropdownEntryConfiguration), typeDiscriminator: "Dropdown")]
[JsonDerivedType(typeof(MaskedTextEntryConfiguration), typeDiscriminator: "MaskedText")]
[JsonDerivedType(typeof(NumberEntryConfiguration), typeDiscriminator: "Number")]
[JsonDerivedType(typeof(PasswordEntryConfiguration), typeDiscriminator: "Password")]
[JsonDerivedType(typeof(TextEntryConfiguration), typeDiscriminator: "Text")]
[JsonDerivedType(typeof(ImageEntryConfiguration), typeDiscriminator: "Image")]
[JsonDerivedType(typeof(AttachmentEntryConfiguration), typeDiscriminator: "Attachment")]
[JsonDerivedType(typeof(MultilineTextEntryConfiguration), typeDiscriminator: "MultilineText")]
[JsonDerivedType(typeof(CurrencyEntryConfiguration), typeDiscriminator: "Currency")]
[JsonDerivedType(typeof(DateEntryConfiguration), typeDiscriminator: "Date")]
[JsonDerivedType(typeof(HyperlinkEntryConfiguration), typeDiscriminator: "Hyperlink")]
[JsonDerivedType(typeof(PinEntryConfiguration), typeDiscriminator: "Pin")]
public record ItemEntryConfiguration : 
    IItemEntryConfiguration
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Label { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Value { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Width { get; set; } = 296;
}