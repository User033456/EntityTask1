﻿using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp2;

public partial class Translations : Page
{
    public Translations()
    {
        InitializeComponent();
        using (var context = new CCIContext())
        {
            
            LoadTranslations();
            OrdersGrid.ItemsSource =translations;
        }
    }
    ObservableCollection<Translation> translations = new ObservableCollection<Translation>();
    private void LoadTranslations()
    {
        using (var context = new CCIContext())
        {
            var translationsList = context.Translations.ToList();
            translations.Clear();
            foreach (var translation in  translationsList)
            {
                translations.Add(translation);
            }
        }
    }
}