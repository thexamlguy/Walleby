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
            .HasKey(x => x.Id);

        modelBuilder.Entity<ItemEntry>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<ItemEntry>()
            .HasMany(x => x.Tags)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemEntry>()
            .HasMany(x => x.Blobs)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemEntry>()
            .HasOne(x => x.Image)
            .WithOne()
            .HasForeignKey<ItemEntry>()
            .IsRequired(false);

        modelBuilder.Entity<BlobEntry>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<BlobEntry>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");
    }
}