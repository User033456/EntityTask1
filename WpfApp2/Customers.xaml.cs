using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class Customers : Page
{
    public Customers()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            LoadCustomers();
            OrdersGrid.ItemsSource =customers;
        }
    }
    ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
    private void LoadCustomers()
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
    public void UpdateDataGrid()
    {
        LoadCustomers();
        OrdersGrid.ItemsSource = customers;
        
    }
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                var customer = OrdersGrid.SelectedItem as Customer;
                var result = MessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CCIContext())
                    {
                        context.Customers.Remove(customer);
                        for (int i = 0; i < customers.Count; i++)
                        {
                            if (customers[i].Id == customer.Id)
                            {
                                customers.RemoveAt(i);
                                UpdateDataGrid();
                                break;
                            }
                        }
                        context.SaveChanges();
                        MessageBox.Show("Успешное удаление заявки");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите 1 элемент");
            }
        }
        else
        {
            MessageBox.Show("Вы не выбрали элемент");
        }
    }

    private void CreateCustomer_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new CreateCustomer();
        window.ShowDialog();
    }
}