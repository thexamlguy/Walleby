using Microsoft.EntityFrameworkCore;

namespace Wallet.Data;

public interface IConnection;

public class WalletContext(IConnection connection) : DbContext
{
    public DbSet<BlobEntry> Blobs { get; set; }

    public DbSet<ItemEntry> Items { get; set; }

    public DbSet<TagEntry> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"{connection}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemEntry>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<ItemEntry>()
            .HasMany(x => x.Tags)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemEntry>()
            .HasMany(x => x.Blobs)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemEntry>().
        HasOne(i => i.Image)
            .WithOne()
            .HasForeignKey<ItemEntry>(i => i.ImageId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BlobEntry>()
            .HasKey(x => x.Id);
    }
}