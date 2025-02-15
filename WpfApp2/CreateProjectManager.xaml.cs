using System.Windows;

namespace WpfApp2;

public partial class CreateProjectManager : Window
{
    public CreateProjectManager()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        // Проверка всех TextBox на пустоту
        if (TextBox.Text != null && TextBox.Text != "")
        {
            using (var context = new CCIContext())
            {
                // Проверка существования Менеджера заказов по ФИО
                bool Flag = context.ProjectManagers.Any(c => c.Name == TextBox.Text);
                // Если такого менеджера не существует, его можно создать
                if (Flag == false)
                {
                    var manager = new ProjectManager();
                    manager.Name = TextBox.Text;
                    context.ProjectManagers.Add(manager);
                    context.SaveChanges();
                    MessageBox.Show("Менеджер проекта добавлен успешно");
                    Close();
                }
                else
                {
                    MessageBox.Show("Такой менеджер проекта уже существует");
                }
            }
        }
    }
}