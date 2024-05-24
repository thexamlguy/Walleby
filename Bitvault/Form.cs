namespace Bitvault;

public record Form
{
    public ICollection<FormEntry>? Entries { get; set; }
}