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
    /// <summary>
    /// Обработчик нажатия кнопки для изменения нотариуса
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        // Проверка на наличие хоть каких - то данных в TextBox
        if (Formats.isNullTextBox(TextBox))
        {
            using (var context = new CCIContext())
            {
                // Проверка сущещствования введённого нотариуса
                bool Flag = context.Notaries.Any(c => c.Name == TextBox.Text);
                // Если нотариус существует, его можно привязать к заявке
                if (Flag)
                {
                    var Employee = context.Notaries.FirstOrDefault(c => c.Name == TextBox.Text);
                    var order = context.Orders.FirstOrDefault(c => c.Id == Id);
                    order.NotariesID= Employee.Id;
                    context.SaveChanges();
                    CustomMessageBox.Show("Нотариус проекта изменён удачно");
                    Close();
                }
                else
                {
                    CustomMessageBox.Show("Ошибка, такого нотариуса не существует","Ошибка",MessageBoxButton.OK);
                }
            }
        }
        else
        {
            CustomMessageBox.Show("Текстовое поле не было заполнено");
        }
    }
}