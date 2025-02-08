using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2;

public partial class DeleteOrder : Page
{
    //List<Order> orders = new List<Order>();
    ObservableCollection<Order> orders = new ObservableCollection<Order>();
    public DeleteOrder()
    {
        InitializeComponent();
        
        using (var context = new CCIContext())
        {
            
            LoadOrders();
            OrdersGrid.ItemsSource = orders;
        }
    }

    private bool SelectedItemCheck()
    {
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
            
    }
    private void LoadOrders()
    {
        using (var context = new CCIContext())
        {
            var ordersList = context.Orders.ToList();
            orders.Clear();
            foreach (var order in ordersList)
            {
                orders.Add(order);
            }
        }
    }
    private void OrdersGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
       
    }

    public void UpdateDataGrid()
    {
        LoadOrders();
        OrdersGrid.ItemsSource = orders;
        
    }

    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                var order = OrdersGrid.SelectedItem as Order;
                var result = MessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CCIContext())
                    {
                        context.Orders.Remove(order);
                        for (int i = 0; i < orders.Count; i++)
                        {
                            if (orders[i].Id == order.Id)
                            {
                                orders.RemoveAt(i);
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

    private void ChangeStatusMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (SelectedItemCheck())
        {
            var order = OrdersGrid.SelectedItem as Order;
            var window = new ChangeStatus(order.Id);
            window.ShowDialog();
            UpdateDataGrid();
        }
    }
    private void ChangeProjectManagerMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (SelectedItemCheck())
        {
            var order = OrdersGrid.SelectedItem as Order;
            var window = new SetProjectManager(order.Id);
            window.ShowDialog();
            UpdateDataGrid();
        }
    }
    private void ChangeNotariesMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (SelectedItemCheck())
        {
            var order = OrdersGrid.SelectedItem as Order;
            var window = new NotariesChange(order.Id);
            window.ShowDialog();
            UpdateDataGrid();
        }
    }

    private void SetRealEndDateMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (SelectedItemCheck())
        {
            var order = OrdersGrid.SelectedItem as Order;
            var window = new SetRealFinishDate(order.Id);
            window.ShowDialog();
            UpdateDataGrid();
        }
    }
}