using System.Windows;

namespace WpfApp2;

public partial class SetRealFinishDate : Window
{
    public SetRealFinishDate()
    {
        InitializeComponent();
    }

    private DateOnly d;
    public SetRealFinishDate(int id, DateOnly date)
    {
        InitializeComponent();
        Id = id;
        d = date;
    }
    private int Id;
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        // Проверка на наличие хоть каких - то данных в TextBox
        if (Formats.isNullDatePicker(TextBox))
        {
            // проверка формата введённой даты
            if (Formats.DateFormat(TextBox.Text.ToString()))
            {
                if (DateOnly.Parse(TextBox.Text.ToString()) < d)
                {
                    CustomMessageBox.Show("Дата окончания не может быть раньше даты начала");
                }
                else
                {
                    // В случае соответсвия формату её можно внести в заявку
                    using (var context = new CCIContext())
                    {
                    
                        var order = context.Orders.FirstOrDefault(c => c.Id == Id);
                        order.RealEndDate = DateOnly.Parse(TextBox.Text.ToString());
                        context.SaveChanges();
                        CustomMessageBox.Show("Дата окончания работы установлена успешно");
                        Close();
                    }
                }
                
            }
            else
            {
                CustomMessageBox.Show("Неправильный формат даты");
            }
        }
        else
        {
            CustomMessageBox.Show("Текстовое поле не было заполнено");
        }
    }
}