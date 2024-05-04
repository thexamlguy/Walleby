using Microsoft.EntityFrameworkCore;

namespace Bitvault;

public class VaultDbContext(DbContextOptions<VaultDbContext> options) : 
    DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Document> Documents { get; set; }

    public DbSet<Locker> Lockers { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Locker>()
            .HasMany(x => x.Tags)
            .WithOne();

        modelBuilder.Entity<Locker>()
            .HasMany(x => x.Documents)
            .WithOne();
    }
}
