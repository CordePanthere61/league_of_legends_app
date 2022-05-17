using System;
using System.Collections.Generic;
using System.Windows;
using league_of_legends_app.CORE.ViewModels;
using league_of_legends_app.Utils;

namespace league_of_legends_app.Views;

public partial class AuthorFormWindow : Window
{
    public AuthorFormWindow()
    {
        InitializeComponent();
        var adapter = InitializeAdapter();
        AuthorFormViewModel vm = new AuthorFormViewModel(adapter);
        DataContext = vm;
    }

    private WindowAdapter InitializeAdapter()
    {
        var adapter = new WindowAdapter(this);
        return adapter;
    }
}