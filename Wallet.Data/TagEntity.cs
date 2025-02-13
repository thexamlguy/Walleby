﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Data;

[Table("Tags")]
public class TagEntity
{
    [Key]
    public Guid Id { get; set; }

    public string? Name { get; set; }
}