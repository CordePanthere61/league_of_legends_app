using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels
{
    public class MainViewModel : ViewModelBase<MainViewModel>
    {
        private ChampionRepository _championRepository;
        private string? _championName;

        public ICommand TriggerChampion => new DelegateCommand(GetChampionName, CanGetChampionName);
        
        public MainViewModel()
        {
            _championRepository = new ChampionRepository();
        }

        public string? ChampionName
        {
            get => _championName;
            set
            {
                _championName = value;
                NotifyPropertyChanged(vm => vm.ChampionName);
            }
        }

        private void GetChampionName()
        {
            ChampionName = _championRepository.Find(109).Name;
        }

        private bool CanGetChampionName()
        {
            return true;
        }
    }
}