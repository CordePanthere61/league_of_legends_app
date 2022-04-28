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
    public ICommand AddChampionCommand => new DelegateCommand(() => _windowAdapter.Commands["AddChampionCommand"]());
    
    public ChampionListingViewModel(IWindowAdapter adapter)
    {
        this._championRepository = new ChampionRepository();
        this._windowAdapter = adapter;
        FetchModels();
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
}