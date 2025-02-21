using System.Windows;

namespace WpfApp2;

public partial class CreateTranslator : Window
{
    public CreateTranslator()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        // Проверка всех TextBox на пустоту
        if (Formats.isNullTextBox(TextBox) && Formats.isNullTextBox(NotesTextBox))
        {
            using (var context = new CCIContext())
            {
                // Проверка существования переводчика по ФИО
                bool Flag = context.Employees.Any(c => c.Name == TextBox.Text);
                // Если переводчика не существует, его можно создать
                if (Flag == false)
                {
                    var translator = new Employee();
                    translator.Name = TextBox.Text.ToString();
                    translator.Note = NotesTextBox.Text.ToString();
                    context.Employees.Add(translator);
                    context.SaveChanges();
                    CustomMessageBox.Show("Переводчик добавлен успешно");
                    Close();
                }
                else
                {
                    CustomMessageBox.Show("Такой переводчик уже существует");
                }
            }
        }
        else
        {
            CustomMessageBox.Show("Не все текстовые поля были заполнены");
        }
    }
}