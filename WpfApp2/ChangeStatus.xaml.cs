using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace WpfApp2;

public partial class ChangeStatus : Window
{
    public ChangeStatus()
    {
        InitializeComponent();
    }

    private int Id;
    public string Status { get; private set; }
    public ChangeStatus(int id)
    {
        InitializeComponent();
        Id = id;
    }
    // Обработчики нажатия radioButton
    private void radio_button1_click(object sender, RoutedEventArgs e)
    {
        RadioButton important1 = (RadioButton)sender;
        Status = important1.Name.ToString();
        
    }
    private void radio_button2_click(object sender, RoutedEventArgs e)
    {
       Create.IsChecked = false;
       Done.IsChecked = false;
    }
    private void radio_button3_click(object sender, RoutedEventArgs e)
    {
        InWork.IsChecked = false;
       Create.IsChecked = false;
    }
    private void save_click(object sender, RoutedEventArgs e)
    {
        // назначение статуса заявки
        int status;
        if (Status != null)
        {
            if (Status == "Create")
            {
                status = 0;
            }
            else if (Status == "Done")
            {
                status = 2;
            }
            else
            {
                status = 1;
            }
            using (var context = new CCIContext())
            {
                // сохранение такового 
                var order = context.Orders.FirstOrDefault(c => c.Id == Id);
                order.status = status;
                context.Entry(order).State =EntityState.Modified;
                context.SaveChanges();
                CustomMessageBox.Show("Статус заявки изменён удачно");
                Close();
            }
        }
        else
        {
            CustomMessageBox.Show("Не был выбран статус");
        }
        Close();
    }
}