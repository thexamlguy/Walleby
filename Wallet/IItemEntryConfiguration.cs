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
public interface IItemEntryConfiguration<TValue> :
    IItemEntryConfiguration
{
    string? Label { get; set; }

    TValue? Value { get; set; }
}

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
public interface IItemEntryConfiguration;