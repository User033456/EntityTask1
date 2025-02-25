using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class SetTranslator : Window
{
    public SetTranslator()
    {
        InitializeComponent();
    }
    private int Id;
    List<string> translators = new List<string>();
    public SetTranslator(int id)
    {
        InitializeComponent();
        Id = id;
        using (var context = new CCIContext())
        {
            var tList = context.Employees.ToList();
            foreach (var employee in tList)
            {
                translators.Add(employee.Name);
            }
        }
    }
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        // Проверка на наличие хоть каких - то данных в TextBox
        if (Formats.isNullComboBox(comboBox))
        {
            using (var context = new CCIContext())
            {
                // Проверка сущещствования введённого переводчика
                bool Flag = context.Employees.Any(c => c.Name == comboBox.Text);
                // Если переводчик существует, его можно привязать к заявке
                if (Flag)
                {
                    var Employee = context.Employees.FirstOrDefault(c => c.Name == comboBox.Text);
                    var translation = context.Translations.FirstOrDefault(c => c.Id == Id);
                    translation.EmployeeId = Employee.Id;
                    context.SaveChanges();
                    CustomMessageBox.Show("Переводчик назначен успешно");
                    Close();
                }
                else
                {
                    CustomMessageBox.Show("Ошибка, такого переводчика не существует","Ошибка",MessageBoxButton.OK);
                }
            }
        }
        else
        {
            CustomMessageBox.Show("Текстовое поле не было заполнено");
        }
    }
    private void AutoCompleteComboBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = comboBox.Text;
        var russianCulture = new System.Globalization.CultureInfo("ru-RU");
        var filtered = translators
            .Where(name => name.ToLower().Contains(text.ToLower()))
            .OrderBy(name => name, StringComparer.Create(russianCulture, false))
            .ToList();
        var temp = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            if (i == filtered.Count)
            {
                break;
            }
            temp.Add(filtered[i]);
        }
        comboBox.ItemsSource = temp;
        comboBox.IsDropDownOpen = temp.Any();
        if (comboBox.Template.FindName("PART_EditableTextBox", comboBox) is TextBox textBox)
        {
            textBox.SelectionStart = text.Length;
        }
    }
}