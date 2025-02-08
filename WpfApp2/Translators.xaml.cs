using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp2;

public partial class Translators : Page
{
    public Translators()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            
           LoadEmployees();
            OrdersGrid.ItemsSource = employees;
        }
    }
    private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
    private void LoadEmployees()
    {
        using (var context = new CCIContext())
        {
            var employeesList = context.Employees.ToList();
           employees.Clear();
            foreach (var employee in employeesList)
            {
                employees.Add(employee);
            }
        }
    }
}
