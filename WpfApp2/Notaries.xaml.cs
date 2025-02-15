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
    private void LoadNotaries()
    {
        using (var context = new CCIContext())
        {
            var NotariesList = context.Notaries.ToList();
            notaries.Clear();
            foreach (var notary in NotariesList)
            {
               notaries.Add(notary);
            }
        }
    }
    public void UpdateDataGrid()
    {
        LoadNotaries();
        OrdersGrid.ItemsSource = notaries;
        
    }
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                var notary = OrdersGrid.SelectedItem as Notary;
                var result = MessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CCIContext())
                    {
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

    private void AddNotaryButton_OnClick(object sender, RoutedEventArgs e)
    {
        CreateNotary window = new CreateNotary();
        window.ShowDialog();
        UpdateDataGrid();
    }
}