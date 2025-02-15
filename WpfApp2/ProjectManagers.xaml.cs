using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class ProjectManagers : Page
{
    public ProjectManagers()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            
            LoadProjectManagers();
            OrdersGrid.ItemsSource = projectManagers;
        }
    }
    
    ObservableCollection<ProjectManager> projectManagers = new ObservableCollection<ProjectManager>();
    private void LoadProjectManagers()
    {
        using (var context = new CCIContext())
        {
            var projectManagersList = context.ProjectManagers.ToList();
            projectManagers.Clear();
            foreach (var Manager in projectManagersList)
            {
                projectManagers.Add(Manager);
            }
        }
    }
    public void UpdateDataGrid()
    {
        LoadProjectManagers();
        OrdersGrid.ItemsSource = projectManagers;
    }
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                var manager= OrdersGrid.SelectedItem as ProjectManager;
                var result = MessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CCIContext())
                    {
                        context.ProjectManagers.Remove(manager);
                        for (int i = 0; i < projectManagers.Count; i++)
                        {
                            if (projectManagers[i].Id == manager.Id)
                            {
                               projectManagers.RemoveAt(i);
                               
                                break;
                            }
                        }
                        context.SaveChanges();
                        UpdateDataGrid();
                        MessageBox.Show("Успешное удаление менеджера проекта");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите 1 элемент");
            }
        }
        else
        {
            MessageBox.Show("Вы не выбрали элемент");
        }
    }

    private void AddProjectManagerButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new CreateProjectManager();
        window.ShowDialog();
        UpdateDataGrid();
    }
}