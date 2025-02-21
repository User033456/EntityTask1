using System.Windows;

namespace WpfApp2;

public partial class CustomMessageBox : Window
{
    public CustomMessageBox()
    {
        InitializeComponent();
    }
    public MessageBoxResult Result { get; private set; }

    public CustomMessageBox(string message, string title = "", MessageBoxButton buttons = MessageBoxButton.OK)
    {
        InitializeComponent();
        MessageText.Text = message;
        Title = title;
        
        // Настройка кнопок в зависимости от параметра buttons
        switch (buttons)
        {
            case MessageBoxButton.YesNo:
                OkButton.Visibility = Visibility.Collapsed;
                YesButton.Visibility = Visibility.Visible;
                NoButton.Visibility = Visibility.Visible;
                break;
            case MessageBoxButton.OK:
                OkButton.Visibility = Visibility.Visible;
                YesButton.Visibility = Visibility.Collapsed;
                NoButton.Visibility = Visibility.Collapsed;
                break;
        }
    }

    private void YesButton_Click(object sender, RoutedEventArgs e)
    {
        Result = MessageBoxResult.Yes;
        DialogResult = true;
        Close();
    }

    private void NoButton_Click(object sender, RoutedEventArgs e)
    {
        Result = MessageBoxResult.No;
        DialogResult = false;
        Close();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        Result = MessageBoxResult.OK;
        DialogResult = true;
        Close();
    }

    public static MessageBoxResult Show(string message, string title = "", MessageBoxButton buttons = MessageBoxButton.OK)
    {
        var messageBox = new CustomMessageBox(message, title, buttons);
        messageBox.ShowDialog();
        return messageBox.Result;
    }
}