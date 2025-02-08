using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace WpfApp2;

public partial class SetProjectManager : Window
{
    public SetProjectManager()
    {
        InitializeComponent();
    }

    private int Id;
    public SetProjectManager(int OrderId)
    {
        InitializeComponent();
        Id = OrderId;
    }

    private async void Button_OnClick(object sender, RoutedEventArgs e)
    {
        if (TextBox.Text != null && TextBox.Text != "")
        {
            using (var context = new CCIContext())
            {
                bool Flag = context.ProjectManagers.Any(c => c.Name == TextBox.Text);
                if (Flag)
                {
                    var Employee = context.ProjectManagers.FirstOrDefault(c => c.Name == TextBox.Text);
                    var order = context.Orders.FirstOrDefault(c => c.Id == Id);
                    order.ProjectManagerId = Employee.Id;
                    context.SaveChanges();
                    MessageBox.Show("Менеджер проекта изменён удачно");
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка, такого Менеджера не существует","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }
    }
}