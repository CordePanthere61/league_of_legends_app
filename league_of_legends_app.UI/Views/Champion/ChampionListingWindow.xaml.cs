using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using league_of_legends_app.CORE.Repositories;
using league_of_legends_app.CORE.ViewModels;
using league_of_legends_app.Utils;

namespace league_of_legends_app.Views.Champion
{
    
    public partial class ChampionListingWindow : Window
    {
        private ChampionRepository _championRepository;
        private ChampionListingViewModel _viewModel;
        private bool _firstTimeOpened = true;
        public ChampionListingWindow()
        {
            InitializeComponent();
            var adapter = InitializeAdapter();
            _viewModel = new ChampionListingViewModel(adapter);
            DataContext = _viewModel;
            _championRepository = new ChampionRepository();
        }

        private void ReloadList(object sender, EventArgs e)
        {
            if (_firstTimeOpened)
            {
                _firstTimeOpened = false;
                return;
            }
            _viewModel.FetchModels();
        }

        private WindowAdapter InitializeAdapter()
        {
            var adapter = new WindowAdapter(this);
            adapter.Commands = new Dictionary<string, Action>
            {
                ["AddChampionCommand"] = AddChampionCommand
            };
            adapter.CommandsWithId = new Dictionary<string, Action<int>>
            {
                ["EditChampionCommand"] = EditChampionCommand,
                ["DeleteChampionCommand"] = DeleteChampionCommand
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

        private async void DeleteChampionCommand(int championId)
        {
            var champion = await _championRepository.Find(championId);
            if (MessageBox.Show("Do you really want to delete the champion \"" + champion.Name + "\" ?", "Confirmation", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            await _championRepository.Delete(await _championRepository.Find(championId));
            MessageBox.Show(champion.Name + " deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
