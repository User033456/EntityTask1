namespace WpfApp2;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
public class Customer
{
    public int Id { get; set; } // ID
    public string? Name { get; set; } // Имя заказчика/ название организации
    public string? Email { get; set; } // Емеил 
    public bool? isLegalEntity { get; set; } // Юридическое лицо
    public string? Address { get; set; } // Адрес
    public List<Order>? Orders { get; set; }
}
public class Translation
{
    public int Id { get; set; } // ID
    public string Type { get; set; } // тип перевода
    public int WordsCount { get; set; } // количество слов в переводе
    public string OriginLanguage { get; set; } // оригинальный язык
    public string ForeignLanguage { get; set; } // языки, на которые надо переводить 
    public string Notes { get; set; } // примечания к переводу 
    public string OutputFormat { get; set; } // выходной формат данных
    public string InputFormat { get; set; } // входной формат данных 
    public int? EmployeeId { get; set; } // Айди переводчика (ключ)
    public Employee? Translator { get; set; } // переводчик
    public Order order { get; set; } // заказ
    public int OrderId { get; set; } // айди заказа (ключ)
}
public class Order
{
    public int Id { get; set; } // ID
    public int Price { get; set; } // цена перевода
    public DateOnly RequestDate { get; set; } // дата подачи
    public DateOnly PlannedEndDate { get; set; } // плановая дата окончания
    public DateOnly? RealEndDate { get; set; } // Фактическая дата окончания
    public Customer Customer { get; set; } //  Заказчик
    public int CustomerId { get; set; } // айди заказчика (ключ)
    public ProjectManager? Projectmanager { get; set; } // Глава заказа или кто он там
    public int? ProjectManagerId { get; set; } // его айди (ключ)
    public int? NotariesID {get; set;} // Айди нотариуса (ключ)
    public Notary Notaries { get; set; } // нотариус
    public List<Translation> Translations { get; set; } 
    public int? status { get; set; }
}
// Короче, как я подумал. Там же есть глава проекта или что - то такое. Это не совсем переводчик.
// Лучше вынести в отдельный класс управляющего. Мб ошибаюсь. И поскольку в одном заказе может
// быть несколько переводов, а глава проекта один на весь заказ, исполнителями будут переводчики, а для
// глав проекта будет отдельная таблица
public class Employee
{
    public int Id { get; set; } // ID
    public string Name { get; set; } // ФИО
    public string Note { get; set; } // Сведения
    public List<Translation> Translations { get; set; }
}

public class ProjectManager
{
    public int Id { get; set; } // ID
    public string Name { get; set; } // ФИО
    public List<Order> Orders { get; set; }
}
public class Notary
{
    public int Id { get; set; } // ID
    public string Name { get; set; } // ФИО
    public List<Order> Orders { get; set; } 
}