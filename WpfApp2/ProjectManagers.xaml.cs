using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp2;

public partial class ProjectManagers : Page
{
    public ProjectManagers()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            
            LoadNotaries();
            OrdersGrid.ItemsSource = projectManagers;
        }
    }
    
    ObservableCollection<ProjectManager> projectManagers = new ObservableCollection<ProjectManager>();
    private void LoadNotaries()
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
}