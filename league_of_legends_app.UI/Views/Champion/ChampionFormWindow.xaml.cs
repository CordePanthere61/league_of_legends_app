using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using league_of_legends_app.CORE.ViewModels;
using league_of_legends_app.Utils;

namespace league_of_legends_app.Views.Champion;

public partial class ChampionFormWindow : Window
{
    public ChampionFormWindow(int championId = 0)
    {
        InitializeComponent();
        var adapter = InitializeAdapter();
        ChampionFormViewModel vm;
        if (championId != 0)
        {
            vm = new ChampionFormViewModel(adapter, championId);
        }
        else
        {
            vm = new ChampionFormViewModel(adapter);
        }
        DataContext = vm;
    }
    
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private WindowAdapter InitializeAdapter()
    {
        var adapter = new WindowAdapter();
        adapter.Commands = new Dictionary<string, Action>
        {
            ["CancelCommand"] = CancelCommand
        };
        return adapter;
    }

    private void CancelCommand()
    {
        Close();
    }
    
}