namespace Bitvault;

public record ItemConfiguration
{
    public string Name { get; set; } = "";

    public IList<ItemSectionConfiguration>? Sections { get; set; }

    public static ItemConfiguration CreditCard => new()
    {
        Name = "Credit Card",
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Cardholder name"
                    },
                    new DropdownEntryConfiguration
                    {
                        Label = "Type",
                    },
                    new MaskedTextEntryConfiguration
                    {
                        Label = "Card number"
                    },
                    new MaskedTextEntryConfiguration
                    {
                        Label = "Expiry date"
                    },
                    new MaskedTextEntryConfiguration
                    {
                        Label = "Card verification code"
                    },
                }
            }
        }
    };
}