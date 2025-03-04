using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class AdminConsolePage : Page
{
    public AdminConsolePage()
    {
        InitializeComponent();
        UpdateDataGrids();
    }
    ObservableCollection<User> users = new ObservableCollection<User>();
    ObservableCollection<Operator> operators = new ObservableCollection<Operator>();
    /// <summary>
    /// Загрузка всех пользователей
    /// </summary>
    private void LoadUsers()
    {
        users.Clear();
        using (var context = new CCIContext())
        {
            var usersList = context.Users.OrderBy(e => e.Login).ToList();
            foreach (var user in  usersList)
            {
                users.Add(user);
            }
        }
    }
    /// <summary>
    /// Загрузка всех операторов
    /// </summary>
    private void LoadOperators()
    {
        operators.Clear();
        using (var context = new CCIContext())
        {
            var operatorsList = context.Operators.OrderBy(e => e.Login).ToList();
            foreach (var op in operatorsList)
            {
                operators.Add(op);
            }
        }
    }
    /// <summary>
    /// Обновление информации в DataGrid
    /// </summary>
    private void UpdateDataGrids()
    {
        LoadUsers();
        LoadOperators();
        UsersGrid.ItemsSource = users;
        OperatorsGrid.ItemsSource = operators;
    }
    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        // Проверка на наличие одного выбранного элемента в DataGrid
        if (UsersGrid.SelectedItems != null)
        {
            if (UsersGrid.SelectedItems.Count == 1)
            {
                // Получение выбранного пользователя и запрос подтверждения удаления
                var user = UsersGrid.SelectedItem as User;
                var result = CustomMessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                // Удаление
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CCIContext())
                    {
                        context.Users.Remove(user);
                        context.SaveChanges();
                    }
                    CustomMessageBox.Show("Успешное удаление пользователя");
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
        UpdateDataGrids();
    }
    /// <summary>
    /// Удаление оператора
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuItem_OnClick2(object sender, RoutedEventArgs e)
    {
        // Проверка на наличие одного выбранного элемента в DataGrid
        if (OperatorsGrid.SelectedItem != null)
        {
            if (OperatorsGrid.SelectedItems.Count == 1)
            {
                // Получение выбранного оператора и запрос подтверждения удаления
                var op = OperatorsGrid.SelectedItem as Operator;
                var result = CustomMessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                // Удаление
                if (result == MessageBoxResult.Yes)
                {
                    // У программы 1 администратор. Его удаление недопустимо
                    if (op.Login == "Admin")
                    {
                        CustomMessageBox.Show("Удалить администратора нельзя");
                    }
                    else
                    {
                        using (var context = new CCIContext())
                        {
                            context.Operators.Remove(op);
                            context.SaveChanges();
                        }
                        CustomMessageBox.Show("Успешное удаление оператора");
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
        UpdateDataGrids();
    }

    private void AddUserButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new SignInWindow();
        window.ShowDialog();
        UpdateDataGrids();
    }

    private void AddOperatorButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new SignInWindow(true);
        window.ShowDialog();
        UpdateDataGrids();
    }
}