using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2;

public partial class OrdersPage : Page
{
    //List<Order> orders = new List<Order>();
    ObservableCollection<Order> orders = new ObservableCollection<Order>();
    public OrdersPage()
    {
        InitializeComponent();
        
        using (var context = new CCIContext())
        {
            
            LoadOrders();
            OrdersGrid.ItemsSource = orders;
        }
    }
    /// <summary>
    /// Проверка на то, сколько элементов DataGrid выделено
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Загрузка списка заявок
    /// </summary>
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
    /// <summary>
    /// Обновление данных DataGrid
    /// </summary>
    public void UpdateDataGrid()
    {
        LoadOrders();
        OrdersGrid.ItemsSource = orders;
        
    }
    /// <summary>
    /// Удаление заявки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <summary>
    /// Изменение статуса заявки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <summary>
    /// Изменение или назначение менеджера проекта для заявки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <summary>
    /// Измененние или назначения нотариуса заявки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <summary>
    /// Указание даты реального окончания работы над заявкой
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private  async void OrdersGrid_OnSorting(object sender, DataGridSortingEventArgs e)
    {
        string columnName = e.Column.SortMemberPath;
         await using (var context = new CCIContext())
        {
            if (columnName != null)
            {
                if (columnName == "Id")
                {
                     context.Orders.OrderBy(o => o.Id);
                }
                else if (columnName == "RequestDate")
                {
                    context.Orders.OrderBy(o => o.RealEndDate);
                }
                else if (columnName == "PlannedEndDate")
                {
                    context.Orders.OrderBy(o => o.PlannedEndDate);
                }
                else if (columnName == "RealEndDate")
                {
                    context.Orders.OrderBy(o => o.RealEndDate);
                }
                else if (columnName == "status")
                {
                    context.Orders.OrderBy(o => o.status);
                }
                else if (columnName == "ProjectManagerId")
                {
                    context.Orders.OrderBy(o => o.ProjectManagerId);
                }
                else if (columnName == "CustomerId")
                {
                    context.Orders.OrderBy(o => o.CustomerId);
                }
                else if (columnName == "NotariesID")
                {
                    context.Orders.OrderBy(o => o.NotariesID);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}