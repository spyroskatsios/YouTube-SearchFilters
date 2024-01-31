using System.Reflection;
using Customers.Entities;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Customers.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Customer> Customers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    
    public async Task SaveAsync(CancellationToken cancellationToken) => await base.SaveChangesAsync(cancellationToken);
}

public interface IAppDbContext
{
    DbSet<Customer> Customers { get; }
    Task SaveAsync(CancellationToken cancellationToken = default);
}