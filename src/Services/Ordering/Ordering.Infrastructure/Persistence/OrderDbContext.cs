using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

internal class OrderDbContext : DbContext
{
    private readonly ICurrentUserContext _currentUserContext;

    public OrderDbContext(
        DbContextOptions<OrderDbContext> options,
        ICurrentUserContext currentUserContext)
        : base(options)
    {
        _currentUserContext = currentUserContext;
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        AutomaticAssignAuditData();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        AutomaticAssignAuditData();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Order>(options =>
        {
            options.Property(t => t.UserName)
                .HasMaxLength(200)
                .IsRequired();
            options.Property(t => t.FirstName)
                .HasMaxLength(100);
            options.Property(t => t.LastName)
                .HasMaxLength(100);
            options.Property(t => t.EmailAddress)
                .HasMaxLength(200);
            options.Property(t => t.AddressLine)
                .HasMaxLength(400);
            options.Property(t => t.Country)
                .HasMaxLength(100);
            options.Property(t => t.State)
                .HasMaxLength(100);
            options.Property(t => t.ZipCode)
                .HasMaxLength(20);
            options.Property(t => t.StripeId)
                .HasMaxLength(200);
            options.Property(t => t.CreatedBy)
                .HasMaxLength(200);
            options.Property(t => t.LastModifiedBy)
                .HasMaxLength(200);
            options.Property(t => t.TotalPrice)
                .HasPrecision(18, 2);

            options.HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(x => x.OrderId);
        });

        builder.Entity<OrderItem>(options =>
        {
            options.Property(t => t.ProductId)
                .HasMaxLength(100)
                .IsRequired();
            options.Property(t => t.ProductName)
                .HasMaxLength(400);
            options.Property(t => t.Category)
                .HasMaxLength(200);
            options.Property(t => t.Summary)
                .HasMaxLength(2000);
            options.Property(t => t.ImageFile)
                .HasMaxLength(400);
            options.Property(t => t.CreatedBy)
                .HasMaxLength(200);
            options.Property(t => t.LastModifiedBy)
                .HasMaxLength(200);

            options.Property(t => t.Price)
                .HasPrecision(18, 2);
            options.Property(t => t.DiscountAmount)
                .HasPrecision(18, 2);
        });
    }

    private void AutomaticAssignAuditData()
    {
        var currentUserName = _currentUserContext.GetCurrentUserName();

        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currentUserName;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = currentUserName;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    break;
            }
        }
    }
}