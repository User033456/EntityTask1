using System.Windows;

namespace WpfApp2;

public partial class NotariesChange : Window
{
    public NotariesChange()
    {
        InitializeComponent();
    }

    private int Id;
    public NotariesChange(int id)
    {
        InitializeComponent();
        Id = id;
    }
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        if (TextBox.Text != null && TextBox.Text != "")
        {
            using (var context = new CCIContext())
            {
                bool Flag = context.Notaries.Any(c => c.Name == TextBox.Text);
                if (Flag)
                {
                    var Employee = context.Notaries.FirstOrDefault(c => c.Name == TextBox.Text);
                    var order = context.Orders.FirstOrDefault(c => c.Id == Id);
                    order.NotariesID= Employee.Id;
                    context.SaveChanges();
                    MessageBox.Show("Нотариус проекта изменён удачно");
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка, такого нотариуса не существует","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }
    }
}