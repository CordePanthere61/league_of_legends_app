using System;
using System.Collections.Generic;
using System.Windows;
using league_of_legends_app.CORE.ViewModels;
using league_of_legends_app.Utils;

namespace league_of_legends_app.Views.Champion
{
    
    public partial class ChampionListingWindow : Window
    {
        public ChampionListingWindow()
        {
            InitializeComponent();
            var adapter = InitializeAdapter();
            var vm = new ChampionListingViewModel(adapter);
            DataContext = vm;
        }

        private WindowAdapter InitializeAdapter()
        {
            var adapter = new WindowAdapter();
            adapter.Commands = new Dictionary<string, Action>
            {
                ["AddChampionCommand"] = AddChampionCommand
            };
            adapter.CommandsWithId = new Dictionary<string, Action<int>>
            {
                ["EditChampionCommand"] = EditChampionCommand 
            };
            return adapter;
        }

        private void AddChampionCommand()
        {
            var window = new ChampionFormWindow();
            window.Show();
        }

        private void EditChampionCommand(int championId)
        {
            var window = new ChampionFormWindow(championId);
            window.Show(); 
        }
    }
}
