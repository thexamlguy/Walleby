﻿using Toolkit.Foundation;

namespace Bitvault;

public record VaultConfiguration : ComponentConfiguration
{
    public string? Name { get; set; }
}