using JewelryManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JewelryManagementApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<HistoryOfEvent> HistoryOfEvents => Set<HistoryOfEvent>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<LoginLog> LoginLogs => Set<LoginLog>();
    public DbSet<Jewellery> Jewelleries => Set<Jewellery>();
    public DbSet<Benefit> Benefits => Set<Benefit>();
    public DbSet<Reminder> Reminders => Set<Reminder>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<VendorDeal> VendorDeals => Set<VendorDeal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Jewellery>()
            .HasOne(j => j.Vendor)
            .WithMany(v => v.Jewelleries)
            .HasForeignKey(j => j.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Jewellery)
            .WithMany(j => j.Transactions)
            .HasForeignKey(t => t.JewelId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VendorDeal>()
            .HasOne(vd => vd.Vendor)
            .WithMany(v => v.Deals)
            .HasForeignKey(vd => vd.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VendorDeal>()
            .HasOne(vd => vd.Jewellery)
            .WithMany(j => j.Deals)
            .HasForeignKey(vd => vd.JewelId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HistoryOfEvent>()
            .Property(x => x.Time)
            .HasColumnType("TEXT");
    }
}
