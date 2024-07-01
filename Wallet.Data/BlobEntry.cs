using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Data;

[Table("Blobs")]
public record BlobEntry
{
    public byte[]? Data { get; set; }

    public int Type { get; set; }

    [Key]
    public Guid Id { get; set; }

    public DateTime DateTime { get; set; }
}