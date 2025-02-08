using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp2;

public partial class Customers : Page
{
    public Customers()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            LoadNotaries();
            OrdersGrid.ItemsSource =customers;
        }
    }
    ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
    private void LoadNotaries()
    {
        using (var context = new CCIContext())
        {
            var customersList = context.Customers.ToList();
            customers.Clear();
            foreach (var Customer in  customersList)
            {
                customers.Add(Customer);
            }
        }
    }
}