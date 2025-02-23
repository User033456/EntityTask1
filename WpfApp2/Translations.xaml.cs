using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class Translations : Page
{
    public Translations()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            
            LoadTranslations();
            OrdersGrid.ItemsSource =translations;
        }
    }
    ObservableCollection<Translation> translations = new ObservableCollection<Translation>();
    private void LoadTranslations()
    {
        using (var context = new CCIContext())
        {
            var translationsList = context.Translations.ToList();
            translations.Clear();
            foreach (var translation in  translationsList)
            {
                translations.Add(translation);
            }
        }
    }
    /// <summary>
    /// Обновление данных в DataGrid
    /// </summary>
    public void UpdateDataGrid()
    {
        LoadTranslations();
        OrdersGrid.ItemsSource = translations;
        
    }
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        // Переводчика можно назначить, если контекстное меню было вызвано при выборе одной строки DataGrid
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                // Получение выбранного перевода из DataGrid
                var translation = OrdersGrid.SelectedItem as Translation;
                var window = new SetTranslator(translation.Id);
                window.ShowDialog();
                UpdateDataGrid();
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
}