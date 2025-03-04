using System.Windows;

namespace WpfApp2;

public partial class ChooseUserStatusInLogin : Window
{
    public ChooseUserStatusInLogin()
    {
        InitializeComponent();
    }

    private void LoginAsUserButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new LoginWindow("User");
        window.Show();
        Close();
    }

    private void LoginAsOperatorButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new LoginWindow("Operator");
        window.Show();
        Close();
    }
}