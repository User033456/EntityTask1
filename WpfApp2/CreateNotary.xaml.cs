using System.Windows;

namespace WpfApp2;

public partial class CreateNotary : Window
{
    public CreateNotary()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        if (TextBox.Text != null && TextBox.Text != "")
        {
            using (var context = new CCIContext())
            {
                bool Flag = context.Notaries.Any(c => c.Name == TextBox.Text);
                if (Flag == false)
                {
                    var notary = new Notary();
                    notary.Name = TextBox.Text;
                    context.Notaries.Add(notary);
                    context.SaveChanges();
                    MessageBox.Show("Нотариус добавлен успешно");
                    Close();
                }
                else
                {
                    MessageBox.Show("Такой нотариус уже существует");
                }
            }
        }
    }
}