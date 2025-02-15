using System.Collections.ObjectModel;
using System.Windows;
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
    /// <summary>
    /// Загрузка таблицы переводчиков в формате ObservableCollection
    /// </summary>
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
    /// <summary>
    /// Обновление данных в DataGrid
    /// </summary>
    public void UpdateDataGrid()
    {
        LoadEmployees();
        OrdersGrid.ItemsSource = employees;
        
    }
    /// <summary>
    /// Обработчик нажатия элемента контекстного меню для удаления переводчика
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        // Переводчика можно удалить, если контекстное меню было вызвано при выборе одной строки DataGrid
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                // Получение выбранного переводчика из DataGrid и запрос на подтверждение удаления
                var translator = OrdersGrid.SelectedItem as Employee;
                var result = MessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CCIContext())
                    {
                        // удаление переводчика и обновление данных в DataGrid и бд
                        context.Employees.Remove(translator);
                        for (int i = 0; i < employees.Count; i++)
                        {
                            if (employees[i].Id == translator.Id)
                            {
                                employees.RemoveAt(i);
                                break;
                            }
                        }
                        context.SaveChanges();
                        UpdateDataGrid();
                        MessageBox.Show("Успешное удаление переводчика");
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

    private void AddTranslatorButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new CreateTranslator();
        window.ShowDialog();
        UpdateDataGrid();
    }
}
