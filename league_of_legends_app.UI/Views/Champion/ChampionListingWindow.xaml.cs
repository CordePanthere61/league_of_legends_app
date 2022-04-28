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
            return adapter;
        }

        private void AddChampionCommand()
        {
            
        }
    }
}
