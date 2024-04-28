using Microsoft.EntityFrameworkCore;

namespace Bitvault;

public class VaultDbContext: DbContext
{
    public DbSet<Locker> Lockers { get; set; }

    public VaultDbContext(DbContextOptions<VaultDbContext> options): base(options)
    {

    }
}
