using System.ComponentModel.DataAnnotations;

namespace Bitvault;

public record Locker
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int State { get; set; }

    public ICollection<Tag>? Tags { get; }

    public Category? Category { get; }

    public ICollection<Document>? Documents { get; }
}