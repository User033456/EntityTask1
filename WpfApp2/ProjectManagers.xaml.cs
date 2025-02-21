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
    /// <summary>
    /// Загрузка таблицы менеджеров в формате ObservableCollection
    /// </summary>
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
    /// <summary>
    /// Обновление данных в DataGrid
    /// </summary>
    public void UpdateDataGrid()
    {
        LoadProjectManagers();
        OrdersGrid.ItemsSource = projectManagers;
    }
    /// <summary>
    /// Обработчик нажатия элемента контекстного меню для удаления менеджера
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        // Менеджера можно удалить, если контекстное меню было вызвано при выборе одной строки DataGrid
        if (OrdersGrid.SelectedItem != null)
        {
            if (OrdersGrid.SelectedItems.Count == 1)
            {
                // Получение выбранного менеджера из DataGrid и запрос на подтверждение удаления
                var manager= OrdersGrid.SelectedItem as ProjectManager;
                var result = CustomMessageBox.Show("Подтвердите удаление","Подтверждение",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CCIContext())
                    {
                        // Удаление и сохранение данных в бд 
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
                        CustomMessageBox.Show("Успешное удаление менеджера проекта");
                    }
                }
            }
            else
            {
                CustomMessageBox.Show("Выберите 1 элемент");
            }
        }
        else
        {
            CustomMessageBox.Show("Вы не выбрали элемент");
        }
    }

    private void AddProjectManagerButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = new CreateProjectManager();
        window.ShowDialog();
        UpdateDataGrid();
    }
}