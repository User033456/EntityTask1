using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class CreateOrderByUserWithPersonalData : Page
{
    public CreateOrderByUserWithPersonalData()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var flag = true;
        var TextBoxes = new List<TextBox>()
        {
            PerevodTipeTextBox, PriseTextBox, WordCounterTextBox, OriginalLanguageTextBox,ForeignLanguageTextBox,
            InputFormatTextBox,OutPutTextBox, NotesTextBox
        };
        // Проверка всех TextBox на наличие хоть каких - то данных
        foreach (var i in TextBoxes)
        {
            if (Formats.isNullTextBox(i) == false)
            {
                flag = false;
                break;
            }
        }
        // Если в TextBox есть хоть какие - то данные и email соответствует формату, можно начинать проверку формата данных
        if (flag && Formats.isNullDatePicker(DateOfCreateOrder) && Formats.isNullDatePicker(PlaneDateOfEndTextBox))
        {
            if (Formats.DateFormat(DateOfCreateOrder.Text.ToString()) 
                && Formats.DateFormat(PlaneDateOfEndTextBox.Text)
                && Formats.OnlyNumbers(PriseTextBox.Text.ToString())
                && Formats.OnlyNumbers(WordCounterTextBox.Text))
            {
                using (var context = new CCIContext())
                {
                    Order order = new Order();
                    Translation translation = new Translation();
                    // Так как эта страница используется, если пользователь уже вводит персональные данные, они явно существуют и их можно найти
                    var user = context.Users.FirstOrDefault(c => c.Login == UserInf.Login);
                    var existingCustomer = context.Customers.FirstOrDefault(c => c.UserId == user.Id);
                    order.CustomerId = existingCustomer.Id;
                    order.RequestDate = DateOnly.Parse(DateOfCreateOrder.Text.ToString());
                    order.PlannedEndDate = DateOnly.Parse(PlaneDateOfEndTextBox.Text.ToString());
                    order.Price = int.Parse(PriseTextBox.Text);
                    order.RealEndDate = null;
                    order.NotariesID = null;
                    order.ProjectManagerId = null;
                    order.Projectmanager = null;
                    order.Notaries = null;
                    order.status = 0;
                    context.Orders.Add(order);
                    context.SaveChanges();
                    // Создание перевода
                    translation.Type = PerevodTipeTextBox.Text;
                    translation.WordsCount = int.Parse(WordCounterTextBox.Text);
                    translation.OriginLanguage = OriginalLanguageTextBox.Text;
                    translation.ForeignLanguage = ForeignLanguageTextBox.Text;
                    translation.InputFormat = InputFormatTextBox.Text;
                    translation.OutputFormat = OutPutTextBox.Text;
                    translation.Notes = NotesTextBox.Text;
                    translation.OrderId =order.Id;
                    context.Translations.Add(translation);
                    context.SaveChanges();
                    CustomMessageBox.Show("Заявка успешно создана");
                }
            }
            else
            {
                CustomMessageBox.Show("Ошибка в формате данных");
            }
        }
        else
        {
            CustomMessageBox.Show("не все поля заполнены", "ошибка", MessageBoxButton.OK);
        }
    }
}