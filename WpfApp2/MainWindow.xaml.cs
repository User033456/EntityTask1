using System.Diagnostics.Eventing.Reader;
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
        using (var context = new CCIContext())
        {
            var existingTheme = context.themes.FirstOrDefault(c => c.id == 1);
            if (existingTheme != null)
            {
                if (existingTheme.mode == true)
                {
                    var newTheme = new ResourceDictionary
                    {
                        Source = darkThemeUri,
                    };
                    isDarkTheme = true;
                    Application.Current.Resources.MergedDictionaries.Add(newTheme);
                }
                else
                {
                    var newTheme = new ResourceDictionary
                    {
                        Source = lightThemeUri,
                    };
                    isDarkTheme = false;
                    Application.Current.Resources.MergedDictionaries.Add(newTheme);
                }
                ThemeSwitchButton.Header = isDarkTheme ? "Светлая тема" : "Тёмная тема";
            }
        }
    }
    //  true = тёмная, false = светлая
    private bool isDarkTheme = true;
    // URI для файлов тем
    private readonly Uri darkThemeUri = new Uri("DarkTheme.xaml", UriKind.Relative);
    private readonly Uri lightThemeUri = new Uri("WhiteThem.xaml", UriKind.Relative);
    private void CreateOrder_OnClick(object sender, RoutedEventArgs e)
    {
        Frame1.Source = new Uri("CreateOrder.xaml", UriKind.Relative);
    }
    private void DeleteOrder_OnClick(object sender, RoutedEventArgs e)
    {
        Frame1.Source = new Uri("OrdersPage.xaml", UriKind.Relative);
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
    /// <summary>
    /// Обработчик нажатия на кнопку смены темы
    /// </summary>
    private void ThemeSwitchButton_Click(object sender, RoutedEventArgs e)
    {
        isDarkTheme = !isDarkTheme;
        SwitchTheme();
        UpdateThemeButton();
    }
    /// <summary>
    /// Смена темы приложения
    /// </summary>
    private void SwitchTheme()
    {
        // Получение текущего словаря
        var oldTheme = Application.Current.Resources.MergedDictionaries[0];
        // Его очистка
        Application.Current.Resources.MergedDictionaries.Clear();
        // Создание нового словаря
        if(isDarkTheme)
        {
            var newTheme = new ResourceDictionary
            {
                Source = darkThemeUri,
            };
            Application.Current.Resources.MergedDictionaries.Add(newTheme);
            using (var context = new CCIContext())
            {
                var theme = context.themes.FirstOrDefault(c => c.id == 1);
                theme.mode = true;
                context.Entry(theme).State =EntityState.Modified;
                context.SaveChanges();
            }
        }
        else
        {
            var newTheme = new ResourceDictionary
            {
                Source = lightThemeUri,
            };
            Application.Current.Resources.MergedDictionaries.Add(newTheme);
            using (var context = new CCIContext())
            {
                var theme = context.themes.FirstOrDefault(c => c.id == 1);
                theme.mode = false;
                context.Entry(theme).State =EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
    private void UpdateThemeButton()
    {
        ThemeSwitchButton.Header = isDarkTheme ? "Светлая тема" : "Тёмная тема";
    }
    private void FeedbackButton_OnClick(object sender, RoutedEventArgs e)
    {
       var window = new FeedbackWindow();
       window.ShowDialog();
    }
}