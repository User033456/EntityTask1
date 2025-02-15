using System.Windows;

namespace WpfApp2;

public partial class CreateCustomer : Window
{
    public CreateCustomer()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        // Проверка всех TextBox на пустоту
        if (Formats.isNullTextBox(NameTextBox) && Formats.isNullTextBox(AdressTextBox) &&
            Formats.isNullTextBox(EmailTextBox))
        {
            // Создание заказчика
            using (var context = new CCIContext())
            {
                var customer = new Customer();
                // Проверка существования заказчика по email 
                bool CustomerFlag = context.Customers.Any(c => c.Email == EmailTextBox.Text);
                // Если таких заказчиков нет, создаётся новый
                if (CustomerFlag == false)
                {
                    customer.Name = NameTextBox.Text;
                    customer.Address = AdressTextBox.Text;
                    customer.Email = EmailTextBox.Text;
                    context.Customers.Add(customer);
                    context.SaveChanges();
                    MessageBox.Show("Заказчик создан успешно");
                    Close();
                }
                else
                {
                    MessageBox.Show("Такой заказчик уже существует");
                }
            }
        }
    }
}