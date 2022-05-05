using System.Net.Http.Headers;
using System.Windows.Input;
using league_of_legends_app.CORE.Interfaces;
using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels;

public class ChampionListingViewModel : ViewModelBase<ChampionListingViewModel>
{
    private ChampionRepository _championRepository;
    private List<Champion> _champions;
    private IWindowAdapter _windowAdapter;
    private Champion? _selectedChampion;
    public ICommand AddChampionCommand => new DelegateCommand(_windowAdapter.Commands["AddChampionCommand"]);
    public ICommand EditChampionCommand => new DelegateCommand(EditChampion);
    
    
    public ChampionListingViewModel(IWindowAdapter adapter)
    {
        _championRepository = new ChampionRepository();
        _windowAdapter = adapter;
        FetchModels();
    }

    public Champion? SelectedChampion
    {
        get => _selectedChampion;
        set
        {
            _selectedChampion = value;
            NotifyPropertyChanged(vm => vm.SelectedChampion);
        }
    }
    
    public List<Champion> Champions
    {
        get => _champions;
        set
        {
            _champions = value;
            NotifyPropertyChanged(vm => vm.Champions);
        }
    }
    
    private async void FetchModels()
    { 
        _champions = await _championRepository.FindAll();
        Champions = _champions;
    }

    private void EditChampion()
    {
        if (SelectedChampion == null)
        {
            _windowAdapter.Error("No champion selected.");
            return;
        }
        _windowAdapter.CommandsWithId["EditChampionCommand"](SelectedChampion.Id);
    }
}