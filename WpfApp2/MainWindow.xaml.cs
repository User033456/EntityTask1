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
using Uri = System.Uri;

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
        Frame1.Source = new Uri("DeleteOrder.xaml", UriKind.Relative);
    }

    private void FindOrder_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void Translators_OnClick(object sender, RoutedEventArgs e)
    {
        Frame1.Source = new Uri("Translators.xaml", UriKind.Relative);
    }

    private void Notaries_OnClick(object sender, RoutedEventArgs e)
    {
        Frame1.Source = new Uri("Notaries.xaml", UriKind.Relative);
    }

    private void ProjectManagers_OnClick(object sender, RoutedEventArgs e)
    {
        Frame1.Source = new Uri("ProjectManagers.xaml", UriKind.Relative);
    }

    private void Translations_OnClick(object sender, RoutedEventArgs e)
    {
        Frame1.Source = new Uri("Translations.xaml", UriKind.Relative);
    }

    private void Customers_OnClick(object sender, RoutedEventArgs e)
    {
        Frame1.Source = new Uri("Customers.xaml", UriKind.Relative);
    }
}