using System.ComponentModel.DataAnnotations;

namespace Bitvault;

public class Tag
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}
