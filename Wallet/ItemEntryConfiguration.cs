using System.Text.Json.Serialization;

namespace Wallet;

[JsonDerivedType(typeof(DropdownEntryCollectionConfiguration), typeDiscriminator: "Dropdown")]
[JsonDerivedType(typeof(MaskedTextEntryConfiguration), typeDiscriminator: "MaskedText")]
[JsonDerivedType(typeof(NumberEntryConfiguration), typeDiscriminator: "Number")]
[JsonDerivedType(typeof(PasswordEntryConfiguration), typeDiscriminator: "Password")]
[JsonDerivedType(typeof(TextEntryConfiguration), typeDiscriminator: "Text")]
[JsonDerivedType(typeof(ImageEntryConfiguration), typeDiscriminator: "Images")]
[JsonDerivedType(typeof(AttachmentEntryCollectionConfiguration), typeDiscriminator: "Attachments")]
[JsonDerivedType(typeof(CommentEntryCollectionConfiguration), typeDiscriminator: "Comments")]
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