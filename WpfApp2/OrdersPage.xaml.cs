using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2;

public partial class OrdersPage : Page
{
    ObservableCollection<Order> orders = new ObservableCollection<Order>();
    public OrdersPage()
    {
        InitializeComponent();
        this.LoginType = UserInf.Logintype;
        this.Login = UserInf.Login;
        UpdateDataGrid();
        // Пользователь не имеет доступа к контекстному меню, так как функции этого меню доступны только операторам
        if (LoginType == "User")
        {
            OrdersGrid.ContextMenu.Visibility = Visibility.Collapsed;
        }
    }
    private string LoginType;
    string Login;
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
        this.LoginType = UserInf.Logintype;
        this.Login = UserInf.Login;
        using (var context = new CCIContext())
        {
            // Для оператора загружаются все заявки
            if (UserInf.Logintype == "Operator")
            {
                var ordersList = context.Orders.OrderBy(c =>c.Id).ToList();
                orders.Clear();
                orders = new ObservableCollection<Order>();
                foreach (var order in ordersList)
                {
                    orders.Add(order);
                }
            }
            // Пользователь може твидеть только свои заявки
            else
            {
                var user = context.Users.FirstOrDefault(u => u.Login == UserInf.Login);
                var customer = context.Customers.FirstOrDefault(c => c.UserId == user.Id);
                var ordersList = context.Orders.Where(o => o.CustomerId == customer.Id).OrderBy(c => c.Id).ToList();
                orders.Clear();
                orders = new ObservableCollection<Order>();
                foreach (var order in ordersList)
                {
                    orders.Add(order);
                }
            }
        }
    }
    /// <summary>
    /// Обновление данных DataGrid
    /// </summary>
    public void UpdateDataGrid()
    {
        LoadOrders();
        if (orders.Count != 0)
        {
            OrdersGrid.ItemsSource = orders;
        }
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
                var result = CustomMessageBox.Show("Подтвердите удаление", "Подтверждение", MessageBoxButton.YesNo);
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
                                break;
                            }
                        }
                        context.SaveChanges();
                        UpdateDataGrid();
                        CustomMessageBox.Show("Успешное удаление заявки");
                    }
                }
            }
            else
            {
                CustomMessageBox.Show("Выберите 1 элемент");
            }
        }
        else
        {
            CustomMessageBox.Show("Вы не выбрали элемент");
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
            var window = new SetRealFinishDate(order.Id, order.RequestDate);
            window.ShowDialog();
            UpdateDataGrid();
        }
    }
    /// <summary>
    ///  Сортировка. ПОКА НЕ РАБОТАЕТ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private  async void OrdersGrid_OnSorting(object sender, DataGridSortingEventArgs e)
    {
    }
}