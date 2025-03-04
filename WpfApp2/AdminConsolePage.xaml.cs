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
    private void UpdateDataGrids()
    {
        LoadUsers();
        LoadOperators();
        UsersGrid.ItemsSource = users;
        OperatorsGrid.ItemsSource = operators;
    }
    
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (UsersGrid.SelectedItems != null)
        {
            if (UsersGrid.SelectedItems.Count == 1)
            {
                var user = UsersGrid.SelectedItem as User;
                var result = CustomMessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
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

    private void MenuItem_OnClick2(object sender, RoutedEventArgs e)
    {
        if (OperatorsGrid.SelectedItem != null)
        {
            if (OperatorsGrid.SelectedItems.Count == 1)
            {
                var op = OperatorsGrid.SelectedItem as Operator;
                var result = CustomMessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
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