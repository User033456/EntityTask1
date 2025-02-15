﻿using System.Windows;

namespace WpfApp2;

public partial class SetRealFinishDate : Window
{
    public SetRealFinishDate()
    {
        InitializeComponent();
    }
    public SetRealFinishDate(int id)
    {
        InitializeComponent();
        Id = id;
    }
    private int Id;
    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        // Проверка на наличие хоть каких - то данных в TextBox
        if (TextBox.Text != null && TextBox.Text != "")
        {
            // проверка формата введённой даты
            if (Formats.DateFormat(TextBox.Text.ToString()))
            {
                // В случае соответсвия формату её можно внести в заявку
                using (var context = new CCIContext())
                {
                    
                    var order = context.Orders.FirstOrDefault(c => c.Id == Id);
                    order.RealEndDate = DateOnly.Parse(TextBox.Text.ToString());
                    context.SaveChanges();
                    MessageBox.Show("Дата окончания работы установлена успешно");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Неправильный формат даты");
            }
        }
    }
}