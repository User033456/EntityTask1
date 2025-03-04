using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public partial class CreateOrder : Page
{
    public CreateOrder() =>InitializeComponent();
    public List<TextBox> TextBoxes = new List<TextBox>();
    
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        // Список всех текстбоксов
        TextBoxes = new List<TextBox>()
        {
            FiotTextBox, EmailTextBox, AdressTextBox,PerevodTipeTextBox, PriseTextBox, WordCounterTextBox, OriginalLanguageTextBox,ForeignLanguageTextBox,
            InputFormatTextBox,OutPutTextBox, NotesTextBox
        };
        bool flag = true;
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
            // Если формат всех дат и текстовых полей правильный, можно перейти к оформлению заявки
            if (Formats.EmailFormat(EmailTextBox.Text.ToString()) && Formats.DateFormat(
                                                                      DateOfCreateOrder.Text.ToString())
                                                                  && Formats.DateFormat(PlaneDateOfEndTextBox.Text
                                                                      .ToString())
                                                                  && Formats.OnlyNumbers(PriseTextBox.Text.ToString())
                                                                  && Formats.OnlyNumbers(WordCounterTextBox.Text))
            {
                using (var context = new CCIContext())
                {
                    Order order = new Order();
                    Customer customer = new Customer();
                    Translation translation = new Translation();
                    // заполнение данных заказчика
                    customer.Name = FiotTextBox.Text;
                    customer.Address = AdressTextBox.Text;
                    customer.Email = EmailTextBox.Text;
                    if (IsLegalTextBox.IsChecked == null)
                    {
                        customer.isLegalEntity = false;
                    }
                    else
                    {
                        customer.isLegalEntity = IsLegalTextBox.IsChecked.Value;
                    }
                    bool CustomerFlag = context.Customers.Any(c => c.Email == EmailTextBox.Text);
                    bool UserFlag = false;
                    // Если заказчика не существует, он будет создан
                    if (CustomerFlag == false)
                    {
                        // Если зашёл пользователь, имеющий учётку, но не вводивший персональные данные
                        if (UserInf.Logintype == "User")
                        {
                            var user = context.Users.FirstOrDefault(c => c.Login == UserInf.Login);
                            customer.UserId = user.Id;
                            user.CutomerId = customer.Id;
                            UserFlag = true;
                        }
                        else
                        {
                            // Если пользователь не существовал до этого, оператор должен создать ему аккаунт
                            var window = new SignInWindow();
                            window.ShowDialog();
                            if (window.DialogResult == true)
                            {
                                UserFlag = true;
                                var UserFromDb = context.Users.FirstOrDefault(c => c.Login == UserInf.TempLogin);
                                UserFromDb.CutomerId = customer.Id;
                                customer.UserId = UserFromDb.Id;
                            }
                        }
                        // Если существует и заказчик и его аккаунт, можно продолжать составлять заявку
                        if (UserFlag == true)
                        {
                            context.Customers.Add(customer);
                            context.SaveChanges();
                            // Поиск созданного заказчика для присвоения его Id заявке
                            var existingCustomer = context.Customers.FirstOrDefault(c => c.Email == EmailTextBox.Text);
                            order.CustomerId = customer.Id;
                            if (UserInf.Logintype == "Operator")
                            {
                                var window = new SignInWindow();
                                window.ShowDialog();
                                if (window.DialogResult == true)
                                {
                                    UserFlag = true;
                                    var UserFromDb = context.Users.FirstOrDefault(c => c.Login == UserInf.TempLogin);
                                    UserFromDb.CutomerId = customer.Id;
                                    customer.UserId = UserFromDb.Id;
                                }
                            }
                        }
                    }
                    // Если заказчик существует, программа найдёт его Id и присвоит заявке
                    else
                    {
                        var existingCustomer = context.Customers.FirstOrDefault(c => c.Email == EmailTextBox.Text);
                        if (existingCustomer != null)
                        {
                            order.CustomerId = existingCustomer.Id;
                            UserFlag = true;
                        }
                    }
                    // На этом этапе заказчик точно существует и можно работать над заявкой
                    if (UserFlag == true)
                    {
                        // Заполнение данных заявки
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
                        // Если вход был от лица пользователя, персональные данные ему вводить больше не нужно. Следующие заявки будут создаваться в другом окне
                        if (UserInf.Logintype == "User")
                        {
                            var mainWindow = Window.GetWindow(this) as MainWindow;
                            var window = new MainWindow(UserInf.Logintype, UserInf.Login);
                            window.Show();
                            mainWindow.Close();
                        }
                    }
                    else
                    {
                        CustomMessageBox.Show("Аккаунт заказчика не был найден или создан");
                    }
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
    private void IsLegalTextBox_OnChecked(object sender, RoutedEventArgs e)
    {
        
    }
}