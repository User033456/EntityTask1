using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class CreateOrder : Page
{
    public CreateOrder()
    {
        InitializeComponent();
        TextBoxes = new List<TextBox>()
        {
            FiotTextBox, EmailTextBox, AdressTextBox,PerevodTipeTextBox, PriseTextBox, WordCounterTextBox, OriginalLanguageTextBox,ForeignLanguageTextBox,
            InputFormatTextBox,OutPutTextBox,DateOfCreateOrder, PlaneDateOfEndTextBox,NotesTextBox
        };
    }

    public List<TextBox> TextBoxes = new List<TextBox>();
    
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
       
        bool flag = true;
        foreach (var i in TextBoxes)
        {
            if (i.Text.Length == 0)
            {
                flag = false;
                break;
            }
        }

        if (flag)
        {
            using (var context = new CCIContext())
            {
                Order order = new Order();
                Customer customer = new Customer();
                Translation translation = new Translation();
                customer.Name = FiotTextBox.Text;
                customer.Address = AdressTextBox.Text;
                customer.Email = EmailTextBox.Text;
                
            }
        }
    }
}