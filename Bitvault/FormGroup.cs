namespace Bitvault;

public record FormGroup : FormEntry
{
    public string? Name { get; set; }

    public ICollection<FormField>? Fields { get; set; }
}