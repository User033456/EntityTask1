namespace WpfApp2;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
public class CCIContext : DbContext
{
    public DbSet <Customer> Customers { get; set; }
    public DbSet <Order> Orders { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet <Translation> Translations { get; set; }
    public DbSet<Notary> Notaries { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Подключение к базе данных SQLite
        optionsBuilder.UseSqlite("Data Source=C:\\projectsC#\\EntityTask1\\WpfApp2\\CCI.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Определение связей
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer) 
            .WithMany(c => c.Orders) 
            .HasForeignKey(o => o.CustomerId);
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Projectmanager)
            .WithMany(e => e.Orders)
            .HasForeignKey(o => o.ProjectManagerId)
            .IsRequired(false);
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Notaries)
            .WithMany(n => n.Orders)
            .HasForeignKey(k => k.NotariesID)
            .IsRequired(false);
        modelBuilder.Entity<Translation>()
            .HasOne(e => e.Translator)
            .WithMany(e => e.Translations)
            .HasForeignKey(o => o.EmployeeId)
            .IsRequired(false);
        modelBuilder.Entity<Translation>()
            .HasOne(o => o.order)
            .WithMany(o => o.Translations)
            .HasForeignKey(o => o.OrderId);
    }
}