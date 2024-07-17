using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Data;

[Table("Items")]
public record ItemEntity
{
    [Key]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public int State { get; set; } = 0;

    public Guid? ImageId { get; set; }

    public BlobEntity? Image { get; set; }

    public required string Category { get; set; }

    public ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();

    public ICollection<BlobEntity> Blobs { get; set; } = new List<BlobEntity>();
}