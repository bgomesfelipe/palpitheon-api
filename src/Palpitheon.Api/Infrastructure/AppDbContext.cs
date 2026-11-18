using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Palpitheon.Api.Domain;
namespace Palpitheon.Api.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Nominee> Nominees => Set<Nominee>();
    public DbSet<Nomination> Nominations => Set<Nomination>();
    public DbSet<UserPick> UserPicks => Set<UserPick>();
    public DbSet<PoolSettings> PoolSettings => Set<PoolSettings>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>(e =>
        {
            e.Property(c => c.Name).HasMaxLength(100);
            e.HasIndex(c => c.Name).IsUnique();
        });

        builder.Entity<Nominee>(e =>
        {
            e.Property(c => c.Name).HasMaxLength(100);
            e.HasIndex(c => c.Name).IsUnique();
        });

        builder.Entity<Nomination>(e =>
        {
            e.HasKey(n => new { n.CategoryId, n.NomineeId });

            e.HasOne(n => n.Category).WithMany(c => c.Nominations)
                .HasForeignKey(n => n.CategoryId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(n => n.Nominee).WithMany(n => n.Nominations)
                .HasForeignKey(n => n.NomineeId).OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(n => n.CategoryId)
                .IsUnique()
                .HasFilter("is_winner")
                .HasDatabaseName("ux_nominations_winner");
        });

        builder.Entity<UserPick>(e =>
        {
            e.HasKey(p => new { p.UserId, p.CategoryId });

            e.HasOne(p => p.User).WithMany(u => u.Picks)
                .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

            e.HasOne(p => p.Nomination).WithMany()
                .HasForeignKey(p => new { p.CategoryId, p.NomineeId })
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<PoolSettings>(e =>
        {
            e.ToTable("pool_settings", t =>
                t.HasCheckConstraint("ck_pool_settings_single_row", "id = 1"));
            e.Property(s => s.Id).ValueGeneratedNever();
            e.HasData(new PoolSettings
            {
                Id = 1,
                VotingOpen = false,
                UpdatedAt = new DateTimeOffset(2026, 11, 18, 0, 0, 0, TimeSpan.Zero)
            });
        });
    }
}
