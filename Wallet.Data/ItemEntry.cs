using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Data;

[Table("Items")]
public record ItemEntry
{
    [Key]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public int State { get; set; } = 0;

    public BlobEntry? Image { get; set; }

    public required string Category { get; set; }

    public ICollection<TagEntry> Tags { get; set; } = new List<TagEntry>();

    public ICollection<BlobEntry> Blobs { get; set;  } = new List<BlobEntry>();
}