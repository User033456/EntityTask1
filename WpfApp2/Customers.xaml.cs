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
    /// <summary>
    /// Загрузка таблицы заказчиков в формате ObservableCollection
    /// </summary>
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
    /// <summary>
    /// Обновление данных в DataGrid
    /// </summary>
    public void UpdateDataGrid()
    {
        LoadCustomers();
        OrdersGrid.ItemsSource = customers;
        
    }
    /// <summary>
    /// Обработчик нажатия элемента контекстного меню для удаления заказчика
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        // Заказчика можно удалить, если контекстное меню было вызвано при выборе одной строки DataGrid
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                // Получение выбранного заказчика из DataGrid и запрос на подтверждение удаления
                var customer = OrdersGrid.SelectedItem as Customer;
                var result = MessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CCIContext())
                    {
                        // удаление заказчика и обновление данных в DataGrid и бд
                        context.Customers.Remove(customer);
                        for (int i = 0; i < customers.Count; i++)
                        {
                            if (customers[i].Id == customer.Id)
                            {
                                customers.RemoveAt(i);
                                break;
                            }
                        }
                        context.SaveChanges();
                        UpdateDataGrid();
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
    /// <summary>
    /// Обработчик нажатия кнопки для создания нового заказчика
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CreateCustomer_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new CreateCustomer();
        window.ShowDialog();
    }
}