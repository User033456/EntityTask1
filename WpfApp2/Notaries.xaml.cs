using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class Notaries : Page
{
    public Notaries()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            LoadNotaries();
            OrdersGrid.ItemsSource = notaries;
        }
    }
    ObservableCollection<Notary> notaries = new ObservableCollection<Notary>();
    /// <summary>
    /// Загрузка таблицы нотариусов в формате ObservableCollection
    /// </summary>
    private void LoadNotaries()
    {
        using (var context = new CCIContext())
        {
            var NotariesList = context.Notaries.OrderBy(n => n.Name).ToList();
            notaries.Clear();
            foreach (var notary in NotariesList)
            {
               notaries.Add(notary);
            }
        }
    }
    /// <summary>
    /// Обновление данных в DataGrid
    /// </summary>
    public void UpdateDataGrid()
    {
        LoadNotaries();
        OrdersGrid.ItemsSource = notaries;
        
    }
    /// <summary>
    /// Обработчик нажатия элемента контекстного меню для удаления нотариуса
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        // Нотариуса можно удалить, если контекстное меню было вызвано при выборе одной строки DataGrid
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                // Получение выбранного нотариуса из DataGrid и запрос на подтверждение удаления
                var notary = OrdersGrid.SelectedItem as Notary;
                var result = CustomMessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CCIContext())
                    {
                        // Удаление и сохранение данных в бд 
                        context.Notaries.Remove(notary);
                        for (int i = 0; i < notaries.Count; i++)
                        {
                            if (notaries[i].Id == notary.Id)
                            {
                                notaries.RemoveAt(i);
                                
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

    private void AddNotaryButton_OnClick(object sender, RoutedEventArgs e)
    {
        CreateNotary window = new CreateNotary();
        window.ShowDialog();
        UpdateDataGrid();
    }
    private void SearchNotaryButton_OnClick(object sender, RoutedEventArgs e)
    {
        var Window = new NotarySearchWindow();
        Window.ShowDialog();
    }
}