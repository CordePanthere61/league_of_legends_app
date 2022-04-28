using System;
using System.Collections.Generic;
using System.Windows;
using league_of_legends_app.CORE.ViewModels;
using league_of_legends_app.Utils;
using league_of_legends_app.Views.Champion;

namespace league_of_legends_app.Views
{

    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            var adapter = InitializeAdapter();
            var vm = new MenuViewModel(adapter);
            DataContext = vm;
        }

        private WindowAdapter InitializeAdapter()
        {
            var adapter = new WindowAdapter();
            var commands = new Dictionary<string, Action>
            {
                ["ManageChampionsCommand"] = OpenChampionListingWindow
            };
            adapter.Commands = commands;
            return adapter;
        }

        private void OpenChampionListingWindow()
        {
            var window = new ChampionListingWindow();
            window.Show();
        }
        
    }
}