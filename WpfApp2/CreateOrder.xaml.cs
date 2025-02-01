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
                customer.isLegalEntity = IsLegalTextBox.IsChecked.Value;
                bool CustomerFlag = context.Customers.Any(c => c.Email == EmailTextBox.Text);
                if (CustomerFlag == false)
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                    var existingCustomer = context.Customers.FirstOrDefault(c => c.Email == EmailTextBox.Text);
                    order.CustomerId = customer.Id;
                }
                else
                {
                    var existingCustomer = context.Customers.FirstOrDefault(c => c.Email == EmailTextBox.Text);
                    if (existingCustomer != null)
                    {
                        order.CustomerId = existingCustomer.Id;
                    }
                }
                if (Formats.DateFormat(DateOfCreateOrder.Text.ToString()) && Formats.DateFormat(PlaneDateOfEndTextBox.Text.ToString()))
                {
                    order.RequestDate = DateTime.Parse(DateOfCreateOrder.Text);
                    order.PlannedEndDate = DateTime.Parse(PlaneDateOfEndTextBox.Text.ToString());
                    order.Price = int.Parse(PriseTextBox.Text);
                    order.RealEndDate = null;
                    order.NotariesID = null;
                    order.ProjectManagerId = null;
                    order.Projectmanager = null;
                    order.Notaries = null;
                    context.Orders.Add(order);
                    context.SaveChanges();
                    translation.Type = PerevodTipeTextBox.Text;
                    if (Formats.OnlyNumbers(WordCounterTextBox.Text))
                    {
                        translation.WordsCount = int.Parse(WordCounterTextBox.Text);
                        translation.OriginLanguage = OriginalLanguageTextBox.Text;
                        translation.ForeignLanguage = ForeignLanguageTextBox.Text;
                        translation.InputFormat = InputFormatTextBox.Text;
                        translation.OutputFormat = OutPutTextBox.Text;
                        translation.Notes = NotesTextBox.Text;
                        var existingCustomer = context.Orders.FirstOrDefault(c => c.Price == int.Parse(PriseTextBox.Text));
                        translation.OrderId =existingCustomer.Id;
                        
                        context.Translations.Add(translation);
                        context.SaveChanges();
                        MessageBox.Show("Заявка успешно создана");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Ошибка в формате данных");
                }
                
            }
        }
        
    }

    private void IsLegalTextBox_OnChecked(object sender, RoutedEventArgs e)
    {
        if (IsLegalTextBox.IsChecked == true)
        {
            IsLegalTextBox.IsChecked = false;
        }
        else
        {
            IsLegalTextBox.IsChecked = true;
        }
    }
}