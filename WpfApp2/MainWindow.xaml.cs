using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace WpfApp2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CreateOrder_OnClick(object sender, RoutedEventArgs e)
    {
        Frame1.Source = new Uri("CreateOrder.xaml", UriKind.Relative);
    }

    private void DeleteOrder_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void FindOrder_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void AllOrders_OnClick(object sender, RoutedEventArgs e)
    {
        
    }
}