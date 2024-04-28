using System.ComponentModel.DataAnnotations;

namespace Bitvault;

public record Locker
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}
