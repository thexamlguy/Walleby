using Bitvault.Data;
using Microsoft.EntityFrameworkCore;

namespace Bitvault.Data;

public class VaultDbContext(DbContextOptions<VaultDbContext> options) : 
    DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Document> Documents { get; set; }

    public DbSet<Content> Items { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>()
            .HasMany(x => x.Tags)
            .WithOne();

        modelBuilder.Entity<Content>()
            .HasMany(x => x.Documents)
            .WithOne();
    }
}
