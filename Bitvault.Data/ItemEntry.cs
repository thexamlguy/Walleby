using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bitvault.Data;

[Table("Items")]
public record ItemEntry
{
    [Key]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public int State { get; set; } = 0;

    public ICollection<TagEntry>? Tags { get; }

    public ICollection<BlobEntry>? Blobs { get; }
}