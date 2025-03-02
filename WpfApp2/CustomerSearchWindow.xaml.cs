using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class CustomerSearchWindow : Window
{
    public CustomerSearchWindow()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            var tList = context.Customers.ToList();
            foreach (var customer in tList)
            {
                customers.Add(customer.Name);
            }
        }
    }
    List<string> customers = new List<string>();

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        if (Formats.isNullComboBox(comboBox))
        {
            using (var context = new CCIContext())
            {
                var Flag = context.Customers.Any(c => c.Name == comboBox.Text);
                if (Flag)
                {
                    var customer = context.Customers.FirstOrDefault(c => c.Name == comboBox.Text);
                    var inf =
                        $"{customer.Name} существует.\nО нём хранится следующая информация:\n{customer.Email}\n{customer.Address}";
                    if (customer.isLegalEntity == true)
                    {
                        inf += "\nЮридическое лицо";
                    }
                    else
                    {
                        inf += "\nФизическое лицо";
                    }

                    CustomMessageBox.Show(inf);
                }
                else
                {
                    CustomMessageBox.Show("Такого заказчика не существует");
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
            var filtered = customers
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