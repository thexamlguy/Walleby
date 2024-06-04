using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bitvault.Data;

[Table("Blobs")]
public record BlobEntry
{
    public byte[]? Data { get; set; }

    public int Type { get; set; }

    [Key]
    public int Id { get; set; }

    public DateTimeOffset DateTime { get; set; }
}