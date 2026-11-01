
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.Core.Models;

namespace Talabat.Repository.Data;

public class dbContext:DbContext
{
    public dbContext(DbContextOptions<dbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }


}
