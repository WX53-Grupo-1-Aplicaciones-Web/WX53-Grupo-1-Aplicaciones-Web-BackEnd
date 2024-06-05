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
    public DbSet<Artisan> Artisans { get; set; } 
    public DbSet<Product> Products { get; set; } 
    public DbSet<Order> Orders { get; set; }



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
        builder.Entity<Customer>().ToTable("Customers");
        builder.Entity<Customer>().HasKey(c=>c.Id);
        builder.Entity<Customer>().Property(c=>c.Name).IsRequired();
        builder.Entity<Customer>().Property(c=>c.LastName).IsRequired();
        builder.Entity<Customer>().Property(c=>c.Phone).IsRequired();
        
        builder.Entity<Artisan>().ToTable("Artisans");
        builder.Entity<Artisan>().HasKey(c=>c.Id);
        builder.Entity<Artisan>().Property(c=>c.Name).IsRequired();
        builder.Entity<Artisan>().Property(c=>c.LastName).IsRequired();
        builder.Entity<Artisan>().Property(c=>c.Phone).IsRequired();
        
        builder.Entity<Product>().ToTable("Products"); 
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Name).IsRequired();
        builder.Entity<Product>().Property(p => p.Unit_Price).IsRequired();
        builder.Entity<Product>().Property(p => p.Stock).IsRequired();
        
        builder.Entity<Order>().ToTable("Orders");
        builder.Entity<Order>().HasKey(o => o.Id);
        builder.Entity<Order>().Property(o => o.request_date).IsRequired();
        builder.Entity<Order>().Property(o => o.shipping_date).IsRequired();
        builder.Entity<Order>().Property(o => o.status).IsRequired();
        builder.Entity<Order>().Property(o => o.delivery_address).IsRequired();
        
    }
}