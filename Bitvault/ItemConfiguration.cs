namespace Bitvault;

public record ItemConfiguration
{
    public IList<ItemSectionConfiguration> Sections { get; set; } = new List<ItemSectionConfiguration>();

    public static ItemConfiguration Identity => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {

        }
    };

    public static ItemConfiguration BankAccount => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {

        }
    };

    public static ItemConfiguration Note => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {

        }
    };


    public static ItemConfiguration Document => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {

        }
    };

    public static ItemConfiguration DrivingLicence => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {

        }
    };
    
    public static ItemConfiguration Login => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Username"
                    },
                    new PasswordEntryConfiguration
                    {
                        Label = "Password"
                    }
                }
            }
        }
    };

    public static ItemConfiguration Password => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {                    
                    new PasswordEntryConfiguration
                    {
                        Label = "Password"
                    }
                }
            }
        }
    };

    public static ItemConfiguration CreditCard => new()
    {
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
                        Values = ["American Express", "Discover", "Maestro", "Mastercard", "Visa"],
                    },
                    new MaskedTextEntryConfiguration
                    {
                        Label = "Card number",
                        Pattern = "0000-0000-0000-0000",
                        Value = "____-____-____-____",
                    },
                    new MaskedTextEntryConfiguration
                    {
                        Label = "Expiry date",
                        Pattern = "00/00",
                        Value = "__/__",
                    },
                    new MaskedTextEntryConfiguration
                    {
                        Label = "Card verification code",
                        Pattern = "000",
                        Value = "___",
                    },
                }
            }
        }
    };
}