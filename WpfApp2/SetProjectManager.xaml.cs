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
        // Проверка на наличие хоть каких - то данных в TextBox
        if (Formats.isNullTextBox(TextBox))
        {
            using (var context = new CCIContext())
            {
                // Проверка сущещствования введённого менеджера
                bool Flag = context.ProjectManagers.Any(c => c.Name == TextBox.Text);
                // Если менеджер существует, его можно привязать к заявке
                if (Flag)
                {
                    var Employee = context.ProjectManagers.FirstOrDefault(c => c.Name == TextBox.Text);
                    var order = context.Orders.FirstOrDefault(c => c.Id == Id);
                    order.ProjectManagerId = Employee.Id;
                    context.SaveChanges();
                    CustomMessageBox.Show("Менеджер проекта изменён удачно");
                    Close();
                }
                else
                {
                    CustomMessageBox.Show("Ошибка, такого Менеджера не существует","Ошибка",MessageBoxButton.OK);
                }
            }
        }
        else
        {
            CustomMessageBox.Show("Текстовое поле не было заполнено");
        }
    }
}