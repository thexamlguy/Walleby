using System.ComponentModel.DataAnnotations;

namespace Bitvault;

public record Document
{
    public byte[]? Blob { get; set; }

    public DocumentType Type { get; set; }

    [Key]
    public int Id { get; set; }
}