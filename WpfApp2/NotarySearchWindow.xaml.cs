using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class NotarySearchWindow : Window
{
    public NotarySearchWindow()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            var tList = context.Notaries.ToList();
            foreach (var notary in tList)
            {
                notaries.Add(notary.Name);
            }
        }
    }
    List<string> notaries = new List<string>();
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        if (Formats.isNullComboBox(comboBox))
        {
            using (var context = new CCIContext())
            {
                bool Flag = context.Notaries.Any(c => c.Name == comboBox.Text);
                if (Flag)
                {
                    var notary = context.Notaries.FirstOrDefault(c => c.Name == comboBox.Text);
                    CustomMessageBox.Show($"{notary.Name} существует");
                }
                else
                {
                    CustomMessageBox.Show("Такого нотариуса не существует");
                }
            }
        }
        else
        {
            CustomMessageBox.Show("Текстовое поле пустое");
        }
    }
    private void AutoCompleteComboBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = comboBox.Text;
        var russianCulture = new System.Globalization.CultureInfo("ru-RU");
        var filtered = notaries
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
    }
}