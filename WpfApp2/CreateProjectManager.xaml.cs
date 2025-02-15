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
        if (TextBox.Text != null && TextBox.Text != "")
        {
            using (var context = new CCIContext())
            {
                bool Flag = context.ProjectManagers.Any(c => c.Name == TextBox.Text);
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