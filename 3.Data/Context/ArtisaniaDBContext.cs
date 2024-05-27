using System.Collections.Immutable;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace _3.Data.Context;


public class ArtisaniaDBContext:DbContext
{
    public ArtisaniaDBContext()
    {
    }
    
    public ArtisaniaDBContext(DbContextOptions<ArtisaniaDBContext> options):base(options)
    {
    }
    
    public DbSet<Customer>Customers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=localhost,3306;Uid=root;pwd=12345678;Database=Artisania", serVersion);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Customer>().ToTable("Customer");
        builder.Entity<Customer>().HasKey(c=>c.Id);
        builder.Entity<Customer>().Property(c=>c.Name).IsRequired();
        builder.Entity<Customer>().Property(c=>c.LastName).IsRequired();
        builder.Entity<Customer>().Property(c=>c.Phone).IsRequired();
        
    }
}