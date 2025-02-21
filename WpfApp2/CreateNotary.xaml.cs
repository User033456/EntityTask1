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
        // Проверка всех TextBox на пустоту
        if (Formats.isNullTextBox(TextBox))
        {
            using (var context = new CCIContext())
            {
                // Проверка существования нотариуса по ФИО
                bool Flag = context.Notaries.Any(c => c.Name == TextBox.Text);
                // Если нотариуса с таким ФИО не существует, он будет создан
                if (Flag == false)
                {
                    var notary = new Notary();
                    notary.Name = TextBox.Text;
                    context.Notaries.Add(notary);
                    context.SaveChanges();
                    CustomMessageBox.Show("Нотариус добавлен успешно");
                    Close();
                }
                else
                {
                    CustomMessageBox.Show("Такой нотариус уже существует");
                }
            }
        }
        else
        {
            CustomMessageBox.Show("Текстовое поле не было заполнено");
        }
    }
}