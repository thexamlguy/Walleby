using System.ComponentModel.DataAnnotations;

namespace Bitvault.Data;

public class Tag
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}
