using System.Windows;

namespace WpfApp2;

public partial class LoginWindow : Window
{
    private string LoginType;
    /// <summary>
    /// Конструктор, получающий тип логина
    /// </summary>
    /// <param name="LoginType"></param>
    public LoginWindow(string LoginType)
    {
        InitializeComponent();
        this.LoginType = LoginType;
        // Если совершается попытка входа от лица оператора, зарегистрироваться самостоятельно нельзя
        if (LoginType == "Operator")
        {
            SignInButton.Visibility = Visibility.Collapsed;
            SignInTextBlock.Visibility = Visibility.Collapsed;
        }
    }

    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Formats.isNullTextBox(LoginTextBox) && Formats.isNullTextBox(PasswordTextBox))
        {
            // Если совершается попытка входа от лица оператора, следует искать в соответствующей таблице
            if (LoginType == "Operator")
            {
                using (var context = new CCIContext())
                {
                    var flag = context.Operators.Any(c => c.Login == LoginTextBox.Text);
                    if (flag)
                    {
                        // Если оператор с таким логином существует и пароль совпал, вход от лица оператора разрешён
                        var user = context.Operators.FirstOrDefault(c => c.Login == LoginTextBox.Text);
                        if (user.Password == PasswordTextBox.Text)
                        {
                            var window = new MainWindow("Operator", LoginTextBox.Text);
                            window.Show();
                            this.Close();
                        }
                        else
                        {
                            CustomMessageBox.Show("Неверный пароль");
                        }
                    }
                    else
                    {
                        CustomMessageBox.Show("Пользователя с таким логином не существует");
                    }
                }
            }
            else
            {
                // Если совершается попытка входа от лица клиента, следует искать в соответствующей таблице
                using (var context = new CCIContext())
                {
                    var flag = context.Users.Any(c => c.Login == LoginTextBox.Text);
                    if (flag)
                    {
                        // Если клиент с таким логином существует и пароль совпал, вход от лица клиента разрешён
                        var user = context.Users.FirstOrDefault(c => c.Login == LoginTextBox.Text);
                        if (user.Password == PasswordTextBox.Text)
                        {
                            var window = new MainWindow("User", LoginTextBox.Text);
                            window.Show();
                            this.Close();
                        }
                        else
                        {
                            CustomMessageBox.Show("Неверный пароль");
                        }
                    }
                    else
                    {
                        CustomMessageBox.Show("Пользователя с таким логином не существует");
                    }
                }
            }
        }
        else
        {
            CustomMessageBox.Show("Не все поля заполнены");
        }
    }
    /// <summary>
    /// Обработчик вызова окна регистрации нового клиента
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SignInButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new SignInWindow();
        window.ShowDialog();
    }
}