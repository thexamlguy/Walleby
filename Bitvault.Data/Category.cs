using System.ComponentModel.DataAnnotations;

namespace Bitvault.Data;

public record Category
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}