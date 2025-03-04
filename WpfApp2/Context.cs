namespace WpfApp2;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.IO;

public class CCIContext : DbContext
{
    public DbSet <Customer> Customers { get; set; }
    public DbSet <Order> Orders { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet <Translation> Translations { get; set; }
    public DbSet<Notary> Notaries { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    public DbSet<theme> themes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Operator> Operators { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Подключение к базе данных SQLite
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CCI.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
        //optionsBuilder.UseNpgsql("Host=localhost;" + "Database=CCI;" + "Username=user;" + "Password=RomaClown;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Определение связей
        // Один заказчик может иметь много заявок. Заявка же имеет одного закачика. Связь один ко многим
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer) 
            .WithMany(c => c.Orders) 
            .HasForeignKey(o => o.CustomerId);
        // Менеджер следит за многими заявками. Заявка имеет одного менеджера. Связь один ко многим
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Projectmanager)
            .WithMany(e => e.Orders)
            .HasForeignKey(o => o.ProjectManagerId)
            .IsRequired(false);
        // Нотариус заверяет много заявок. Заявку же заверяет один нотариус. Связь один ко многим
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Notaries)
            .WithMany(n => n.Orders)
            .HasForeignKey(k => k.NotariesID)
            .IsRequired(false);
        // Переводчик может работать со многими заявками. Заявка же имеет только одного переводчика. Связь один ко многим
        modelBuilder.Entity<Translation>()
            .HasOne(e => e.Translator)
            .WithMany(e => e.Translations)
            .HasForeignKey(o => o.EmployeeId)
            .IsRequired(false);
        // Заявка может быть сразу на несколько переводов. Перевод же относится к одной заявке. Связь один ко многим(В данном программе не исп)
        modelBuilder.Entity<Translation>()
            .HasOne(o => o.order)
            .WithMany(o => o.Translations)
            .HasForeignKey(o => o.OrderId);
        // Заказчик имеет одну учётку, учётка имеет одного заказчика. Связь 1 к 1
        modelBuilder.Entity<Customer>().HasOne(o => o.user).WithOne(o => o.customer)
            .HasForeignKey<Customer>(o => o.UserId).IsRequired(false);
    }
}