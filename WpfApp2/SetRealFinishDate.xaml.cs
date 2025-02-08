using System.Windows;

namespace WpfApp2;

public partial class SetRealFinishDate : Window
{
    public SetRealFinishDate()
    {
        InitializeComponent();
    }
    public SetRealFinishDate(int id)
    {
        InitializeComponent();
        Id = id;
    }
    private int Id;
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        if (TextBox.Text != null && TextBox.Text != "")
        {
            if (Formats.DateFormat(TextBox.Text.ToString()))
            {
                using (var context = new CCIContext())
                {
                    var order = context.Orders.FirstOrDefault(c => c.Id == Id);
                    order.RealEndDate = DateOnly.Parse(TextBox.Text.ToString());
                    context.SaveChanges();
                    MessageBox.Show("Дата окончания работы установлена успешно");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Неправильный формат даты");
            }
        }
    }
}