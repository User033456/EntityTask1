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
        if (Formats.isNullTextBox(NameTextBox) && Formats.isNullTextBox(AdressTextBox) &&
            Formats.isNullTextBox(EmailTextBox))
        {
            using (var context = new CCIContext())
            {
                var customer = new Customer();
                bool CustomerFlag = context.Customers.Any(c => c.Email == EmailTextBox.Text);
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