using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class SignInWindow : Window
{
    public SignInWindow()
    {
        InitializeComponent();
    }
    bool isOperatorFlag = false;
    public SignInWindow(bool isOperator)
    {
        InitializeComponent();
        this.isOperatorFlag = isOperator;
    }
    bool Flag = false;
    /// <summary>
    /// Конструктор, вызываемый оператором для создания аккаунта пользователю, если таковой его не имеет
    /// </summary>
    /// <param name="user"></param>
    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        using (var context = new CCIContext())
        {
            var flag = context.Users.Any(c => c.Login == LoginTextBox.Text);
            var flag2 = context.Operators.Any(c => c.Login == LoginTextBox.Text);
            if (flag || flag2)
            {
                CustomMessageBox.Show("Такой пользователь уже существует");
            }
            else
            {
                if (isOperatorFlag)
                {
                    var op = new Operator();
                    op.Login = LoginTextBox.Text;
                    op.Password = PasswordTextBox.Text;
                    context.Operators.Add(op);
                    context.SaveChanges();
                    CustomMessageBox.Show("Оператор добавлен успешно");
                    Close();
                }
                else
                {
                    var user = new User();
                    user.Login = LoginTextBox.Text;
                    user.Password = PasswordTextBox.Text;
                    context.Users.Add(user);
                    context.SaveChanges();
                    UserInf.TempLogin = user.Login;
                    DialogResult = true;
                    CustomMessageBox.Show("Поьзователь создан успешно");
                    Close();
                }
            }
        }
    }
}