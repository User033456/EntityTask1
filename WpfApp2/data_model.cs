namespace WpfApp2;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } // Имя заказчика/ название организации
    public string Email { get; set; } // Емеил 
    public bool isLegalEntity { get; set; } // Юридическое лицо
    public string Address { get; set; } // Адрес
}
public class Translation
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int WordsCount { get; set; }
    public string OriginLanguage { get; set; }
    public string ForeignLanguage { get; set; }
    public string Notes { get; set; }
    public string Price { get; set; }
    public string OutputFormat { get; set; }
    public string InputFormat { get; set; }
    public int EmployeeId { get; set; }
    public Employee Translator { get; set; }
    
}
public class Order
{
    public int Id { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime PlannedEndDate { get; set; }
    public DateTime RealEndDate { get; set; }
    public int CustomerId { get; set; }
    public List<int> EmployeeIds { get; set; }
    public int NotariesID {get; set;}
    public Notaries Notaries { get; set; }
    public List<Employee> Employers { get; set; }
    public Customer Customer { get; set; }
}
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
}
public class Notaries
{
    public int Id { get; set; }
    public string Name { get; set; }
    
}