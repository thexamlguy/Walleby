namespace Bitvault;

public record FormField : FormEntry
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public object? Value { get; set; }

    public FormFieldType Type { get; set; }
}