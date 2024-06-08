namespace Bitvault;

public record ItemConfiguration
{
    public IList<ItemSectionConfiguration> Sections { get; set; } = new List<ItemSectionConfiguration>();

    public static ItemConfiguration Identity => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "First name"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Last name"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Date of birth"
                    },
                    new DropdownEntryConfiguration
                    {
                        Label = "Gender",
                        Values = ["Male", "Female", "Non-binary", "Prefer not to say", "Other"]
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Address"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "City"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "State/Province/Region"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Postal code"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Country"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Email address"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Phone number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "National ID number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Nationality"
                    }
                }
            }
        }
    };

    public static ItemConfiguration BankAccount => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new NumberEntryConfiguration
                    {
                        Label = "Name on account"
                    },
                    new DropdownEntryConfiguration
                    {                        
                        Label = "Type",
                        Values = [
                            "Checking Account",
                            "Savings Account",
                            "Money Market Account",
                            "Certificate of Deposit (CD)",
                            "Individual Retirement Account (IRA)",
                            "Joint Account",
                            "Student Account",
                            "Senior Account",
                            "Business Checking Account",
                            "Business Savings Account",
                            "Merchant Account",
                            "Escrow Account",
                            "Trust Account",
                            "Estate Account",
                            "Custodial Account",
                            "Offshore Account",
                            "Online-Only Account",
                            "High-Yield Savings Account",
                            "Health Savings Account (HSA)",
                            "Foreign Currency Account",
                            "Non-Resident External (NRE) Account",
                            "Non-Resident Ordinary (NRO) Account"
                            ]
                    },
                    new NumberEntryConfiguration
                    {
                        Label = "Sort code",
                        MaxLength = 6
                    },
                    new NumberEntryConfiguration
                    {
                        Label = "Account number",
                        MaxLength = 12
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Bank name"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Branch address"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Date opened"
                    },
                    new CurrencyEntryConfiguration
                    {
                        Label = "Current balance"
                    },
                    new HyperlinkEntryConfiguration
                    {
                        Label = "Website"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Phone number"
                    }
                }
            }
        }
    };

    public static ItemConfiguration Note => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration Document => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new ()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Description",
                    },
                    new AttachmentEntryConfiguration
                    {
                        Label = "Attachments",
                    }
                }
            }
        }
    };


    public static ItemConfiguration DrivingLicence => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Surname"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Given Names"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Date of Birth"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Address"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Country"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Class"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Expiry Date"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Issue Date"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Issuing Authority"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Restrictions/Endorsements"
                    }
                }
            }
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
                    },
                    new HyperlinkEntryConfiguration
                    {
                        Label = "Website"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Security Questions"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Two-factor Authentication (2FA)"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
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
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Expiration Date"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration ApiCredentials => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "API Name"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "API Key"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "API Secret"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "API Token"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Description"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Environment"
                    },
                    new HyperlinkEntryConfiguration
                    {
                        Label = "Endpoint URL"
                    },
                    new HyperlinkEntryConfiguration
                    {
                        Label = "Documentation URL"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Additional Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration EducationRecord => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Degree/Certificate"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Institution"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Date Awarded"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "GPA/Grade"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Major/Field of Study"
                    },
                    new AttachmentEntryConfiguration
                    {
                        Label = "Attachments"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration Utility => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Service Provider"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Account Number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Billing Address"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Contact Phone Number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Account Balance"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Due Date"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration Membership => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Membership Type"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Membership Number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Organization"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Join Date"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Renewal Date"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Membership Benefits"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Contact Information"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration InsuranceDocuments => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Policy Holder"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Policy Number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Insurance Company"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Type of Insurance"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Policy Start Date"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Policy End Date"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Coverage Details"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Contact Information"
                    },
                    new AttachmentEntryConfiguration
                    {
                        Label = "Attachments"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration TravelDocuments => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Document Type"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Document Number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Issuing Country"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Issue Date"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Expiration Date"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Issuing Authority"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Purpose/Details"
                    },
                    new AttachmentEntryConfiguration
                    {
                        Label = "Attachments"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration CryptoWallet => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Wallet Name"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Wallet Type"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Wallet Address"
                    },
                    new PasswordEntryConfiguration
                    {
                        Label = "Private Key"
                    },
                    new PasswordEntryConfiguration
                    {
                        Label = "Mnemonic Phrase"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Public Key"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Exchange Address"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Backup Seed Phrase"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration Passport => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Passport Number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Issuing Country"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Nationality"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Surname"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Given Names"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Date of Birth"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Sex"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Place of Birth"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Issue Date"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Expiration Date"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Issuing Authority"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Observations"
                    },
                    new ImageEntryConfiguration
                    {
                        Label = "Passport Photo"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
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
                        Label = "Cardholder"
                    },
                    new DropdownEntryConfiguration
                    {
                        Label = "Type",
                        Values = ["American Express", "Discover", "Maestro", "Mastercard", "Visa"],
                    },
                    new MaskedTextEntryConfiguration
                    {
                        Label = "Card Number",
                        Pattern = "0000-0000-0000-0000",
                        Value = "____-____-____-____",
                    },
                    new MaskedTextEntryConfiguration
                    {
                        Label = "Expiry Date",
                        Pattern = "00/00",
                        Value = "__/__",
                    },
                    new MaskedTextEntryConfiguration
                    {
                        Label = "Valid From",
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
            },
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Pin"
                    },
                    new PinEntryConfiguration
                    {
                        Label = "Credit Limit",
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Interest Rates",
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Rewards"
                    }
                }
            },
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Card Issuer"
                    },
                    new NumberEntryConfiguration
                    {
                        Label = "Phone",
                    },
                    new HyperlinkEntryConfiguration
                    {
                        Label = "Website",
                    }
                }
            }
        }
    };

    public static ItemConfiguration Server => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "IP Address"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Hostname"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Port"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Protocol"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Username"
                    },
                    new PasswordEntryConfiguration
                    {
                        Label = "Password"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "SSH Key Path"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Connection URL"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Remote Desktop URL"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration Database => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "Database Name"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Database Type"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Host"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Port"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Username"
                    },
                    new PasswordEntryConfiguration
                    {
                        Label = "Password"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Connection URL"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Admin URL"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Admin Username"
                    },
                    new PasswordEntryConfiguration
                    {
                        Label = "Admin Password"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Backup Location"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

    public static ItemConfiguration SoftwareLicense => new()
    {
        Sections = new List<ItemSectionConfiguration>
        {
            new()
            {
                Entries = new List<ItemEntryConfiguration>
                {
                    new TextEntryConfiguration
                    {
                        Label = "License Key/Serial Number"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Licensed To"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "License Type"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Purchase Date"
                    },
                    new DateEntryConfiguration
                    {
                        Label = "Expiration Date"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Vendor/Supplier"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Support Contact"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Support Email"
                    },
                    new NumberEntryConfiguration
                    {
                        Label = "Support Phone"
                    },
                    new TextEntryConfiguration
                    {
                        Label = "Usage Restrictions"
                    },
                    new MultilineTextEntryConfiguration
                    {
                        Label = "Notes"
                    }
                }
            }
        }
    };

}