using Microsoft.EntityFrameworkCore;

namespace Bitvault.Data;

public class LockerContext(DbContextOptions<LockerContext> options) :
    DbContext(options)
{
    public DbSet<BlobEntry> Blobs { get; set; }

    public DbSet<ItemEntry> Items { get; set; }

    public DbSet<TagEntry> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemEntry>()
            .HasMany(x => x.Tags)
            .WithOne();

        modelBuilder.Entity<ItemEntry>()
            .HasMany(x => x.Blobs)
            .WithOne();
    }
}