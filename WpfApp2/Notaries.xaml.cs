using System.Collections.ObjectModel;
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
}