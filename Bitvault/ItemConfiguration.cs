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

        }
    };

    public static ItemConfiguration Password => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {

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