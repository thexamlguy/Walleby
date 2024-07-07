using Microsoft.EntityFrameworkCore;

namespace Wallet.Data;

public interface IConnection;

public class WalletContext(IConnection connection) : DbContext
{
    public DbSet<BlobEntity> Blobs { get; set; }

    public DbSet<ItemEntity> Items { get; set; }

    public DbSet<TagEntity> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"{connection}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemEntity>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<ItemEntity>()
            .HasMany(x => x.Tags)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemEntity>()
            .HasMany(x => x.Blobs)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemEntity>().
        HasOne(i => i.Image)
            .WithOne()
            .HasForeignKey<ItemEntity>(i => i.ImageId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BlobEntity>()
            .HasKey(x => x.Id);
    }
}