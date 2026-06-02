using Microsoft.EntityFrameworkCore;
using TodoApi.Entities;

namespace TodoApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
            entity.Property(t => t.Description).HasMaxLength(1000);
            entity.Property(t => t.Category).HasMaxLength(100);
            entity.Property(t => t.Priority).HasConversion<int>();
            entity.Property(t => t.CreatedTime).IsRequired();
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    { ApplyAudit(); return base.SaveChangesAsync(ct); }

    public override int SaveChanges() { ApplyAudit(); return base.SaveChanges(); }

    private void ApplyAudit()
    {
        foreach (var entry in ChangeTracker.Entries<TodoItem>())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.CreatedTime == default)
                    entry.Entity.CreatedTime = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.EditedTime = DateTime.UtcNow;
                entry.Property(e => e.CreatedTime).IsModified = false;
            }
        }
    }
}