using Microsoft.EntityFrameworkCore;

namespace Wallet.Data;

public class WalletContext(DbContextOptions<WalletContext> options) :
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