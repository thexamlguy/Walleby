using Microsoft.EntityFrameworkCore;

namespace Bitvault.Data;

public class ContainerDbContext(DbContextOptions<ContainerDbContext> options) : 
    DbContext(options)
{
    public DbSet<Document> Documents { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .HasMany(x => x.Tags)
            .WithOne();

        modelBuilder.Entity<Item>()
            .HasMany(x => x.Documents)
            .WithOne();
    }
}
