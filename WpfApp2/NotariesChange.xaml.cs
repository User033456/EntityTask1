using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class NotariesChange : Window
{
    public NotariesChange()
    {
        InitializeComponent();
    }
    List<string> NotariesNames = new List<string>();
    private int Id;
    public NotariesChange(int id)
    {
        InitializeComponent();
        Id = id;
        using (var context = new CCIContext())
        {
            var tList = context.ProjectManagers.ToList();
            foreach (var employee in tList)
            {
               NotariesNames.Add(employee.Name);
            }
        }
    }
    /// <summary>
    /// Обработчик нажатия кнопки для изменения нотариуса
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        // Проверка на наличие хоть каких - то данных в TextBox
        if (Formats.isNullComboBox(comboBox))
        {
            using (var context = new CCIContext())
            {
                // Проверка сущещствования введённого нотариуса
                bool Flag = context.Notaries.Any(c => c.Name == comboBox.Text);
                // Если нотариус существует, его можно привязать к заявке
                if (Flag)
                {
                    var Employee = context.Notaries.FirstOrDefault(c => c.Name == comboBox.Text);
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
    private void AutoCompleteComboBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = comboBox.Text;
        var russianCulture = new System.Globalization.CultureInfo("ru-RU");
        var filtered = NotariesNames
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
        // Если удалось найти текстовое поле внутри ComboBox
        if (comboBox.Template.FindName("PART_EditableTextBox", comboBox) is TextBox textBox)
        {
            // курсор в конец введенного текста
            textBox.SelectionStart = text.Length;
        }
    }
}