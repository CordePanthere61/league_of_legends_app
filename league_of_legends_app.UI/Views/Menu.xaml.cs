using System;
using System.Collections.Generic;
using System.Windows;
using league_of_legends_app.CORE.ViewModels;

namespace league_of_legends_app.Views
{

    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            var listeners = InitializeListeners();
            var vm = new MenuViewModel(listeners);
            DataContext = vm;
        }

        private Dictionary<string, Action> InitializeListeners()
        {
            return new Dictionary<string, Action>
            {
                ["ManageChampionsCommand"] = OpenChampionsManagementWindow
            };
        }

        private void OpenChampionsManagementWindow()
        {
            var window = new ChampionsManagement();
            window.Show();
        }
        
    }
}