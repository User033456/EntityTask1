using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class TranslatorSearchWindow : Window
{
    public TranslatorSearchWindow()
    {
        // Получение списка переводчиков
        InitializeComponent();
        using (var context = new CCIContext())
        {
            var tList = context.Employees.ToList();
            foreach (var employee in tList)
            {
                translators.Add(employee.Name);
            }
        }
    }

    public TranslatorSearchWindow(List<string> list)
    {
        InitializeComponent();
        translators = list;
    }
    List<string> translators = new List<string>();
    /// <summary>
    /// Обработчик нажатия кнопки поиска
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        if (Formats.isNullComboBox(comboBox))
        {
            using (var context = new CCIContext())
            {
                // Если переводчик найден, информацию о нём можно вывести
                bool Flag = context.Employees.Any(c => c.Name == comboBox.Text);
                if (Flag)
                {
                    var Employee = context.Employees.FirstOrDefault(c => c.Name == comboBox.Text);
                    CustomMessageBox.Show($"{Employee.Name} существует. О нём хранится следующая информация:{Employee.Note} ");
                }
                else
                {
                    CustomMessageBox.Show("Такого переводчика не существует");
                }
            }
        }
        else
        {
            CustomMessageBox.Show("Текстовое поле пустое");
        }
    }
    /// <summary>
    /// Обработчик, необходимый для выпадающего списка ComboBox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AutoCompleteComboBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = comboBox.Text;
        var russianCulture = new System.Globalization.CultureInfo("ru-RU");
        // Получение отфильтрованного списка, отсортированного по алфавиту
        var filtered = translators
            .Where(name => name.ToLower().Contains(text.ToLower()))
            .OrderBy(name => name, StringComparer.Create(russianCulture, false))
            .ToList();
        var temp = new List<string>();
        // Загрузка первых 4 элементов в выпадающий список
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
    }
}