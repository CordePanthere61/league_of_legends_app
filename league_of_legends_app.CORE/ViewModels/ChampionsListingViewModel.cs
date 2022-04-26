using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels;

public class ChampionsManagementViewModel : ViewModelBase<ChampionsManagementViewModel>
{
    private ChampionRepository _championRepository;
    private List<Champion> _champions;
    
    public ChampionsManagementViewModel()
    {
        this._championRepository = new ChampionRepository();
        FetchModels();
    }

    private async void FetchModels()
    { 
        _champions = await _championRepository.FindAll();
        Champions = _champions;
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
}