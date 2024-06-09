using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Data;

[Table("Tags")]
public class TagEntry
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}