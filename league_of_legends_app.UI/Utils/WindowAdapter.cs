using System;
using System.Collections.Generic;
using System.Windows;
using league_of_legends_app.CORE.Interfaces;

namespace league_of_legends_app.Utils;

public class WindowAdapter : IWindowAdapter
{
    public Dictionary<string, Action> Commands { get; set; }
    public Dictionary<string, Action<int>> CommandsWithId { get; set; }
    private Window _window;

    public WindowAdapter(Window window)
    {
        _window = window;
    }
    public void Error(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void Success(string message)
    {
        MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public void Close()
    {
        _window.Close();
    }
}