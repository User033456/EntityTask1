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
    /// <summary>
    /// Конструктор, вызываемый админом, для регистрации оператора
    /// </summary>
    /// <param name="isOperator"></param>
    public SignInWindow(bool isOperator)
    {
        InitializeComponent();
        this.isOperatorFlag = isOperator;
    }
    bool Flag = false;
    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        using (var context = new CCIContext())
        {
            // Проверка на отсутствие логина, он должен быть уникален
            var flag = context.Users.Any(c => c.Login == LoginTextBox.Text);
            var flag2 = context.Operators.Any(c => c.Login == LoginTextBox.Text);
            if (flag || flag2)
            {
                CustomMessageBox.Show("Такой пользователь уже существует");
            }
            else
            {
                // Добавлен админа от лица админа
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
                // Добавление пользователя от лица пользователя или админа, или оператора
                else
                {
                    var user = new User();
                    user.Login = LoginTextBox.Text;
                    user.Password = PasswordTextBox.Text;
                    context.Users.Add(user);
                    context.SaveChanges();
                    UserInf.TempLogin = user.Login;
                    DialogResult = true;
                    CustomMessageBox.Show("Пользователь создан успешно");
                    Close();
                }
            }
        }
    }
}