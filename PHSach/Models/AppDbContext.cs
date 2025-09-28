using Microsoft.EntityFrameworkCore;
using PHSach.Models.EntityModel;


namespace PHSach.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<WorkYear> WorkYears { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitBudget> UnitBudgets { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BookBatch> BookBatches { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint: UnitId + WorkYearId in UnitBudget
            modelBuilder.Entity<UnitBudget>()
                .HasIndex(ub => new { ub.UnitId, ub.WorkYearId })
                .IsUnique();

            // Relationships
            modelBuilder.Entity<UnitBudget>()
                .HasOne(ub => ub.Unit)
                .WithMany(u => u.UnitBudgets)
                .HasForeignKey(ub => ub.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UnitBudget>()
                .HasOne(ub => ub.WorkYear)
                .WithMany(wy => wy.UnitBudgets)
                .HasForeignKey(ub => ub.WorkYearId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Batch>()
                .HasOne(b => b.WorkYear)
                .WithMany(wy => wy.Batches)
                .HasForeignKey(b => b.WorkYearId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookBatch>()
                .HasOne(bb => bb.Batch)
                .WithMany(b => b.BookBatches)
                .HasForeignKey(bb => bb.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Allocation>()
                .HasOne(a => a.Batch)
                .WithMany(b => b.Allocations)
                .HasForeignKey(a => a.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Allocation>()
                .HasOne(a => a.BookBatch)
                .WithMany(bb => bb.Allocations)
                .HasForeignKey(a => a.BookBatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Allocation>()
                .HasOne(a => a.UnitBudget)
                .WithMany(ub => ub.Allocations)
                .HasForeignKey(a => a.UnitBudgetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
