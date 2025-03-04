using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class ProjectmanagerSearchWindow : Window
{
    public ProjectmanagerSearchWindow()
    {
        // Получение списка менеджеров
        InitializeComponent();
        using (var context = new CCIContext())
        {
            var tList = context.ProjectManagers.ToList();
            foreach (var manager in tList)
            {
               managers.Add(manager.Name);
            }
        }
    }
    private List<string> managers = new List<string>();
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
                // Если менеджер найден, информацию о нём можно вывести
                var Flag = context.ProjectManagers.Any(c => c.Name == comboBox.Text);
                if (Flag)
                {
                    var Manager = context.ProjectManagers.FirstOrDefault(c => c.Name == comboBox.Text);
                    CustomMessageBox.Show($"{Manager.Name} существует.");
                }
                else
                {
                    CustomMessageBox.Show("Такого менеджера не существует");
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
        // Получение отфильтрованного списка, отсортированного по алфавиту
        var russianCulture = new System.Globalization.CultureInfo("ru-RU");
        var filtered = managers
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