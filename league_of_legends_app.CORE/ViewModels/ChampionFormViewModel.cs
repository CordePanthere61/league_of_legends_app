using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels;

public class ChampionFormViewModel : ViewModelBase<ChampionFormViewModel>
{
    private string _name;
    private string _alias;
    private DateTime _releaseDate;
    private int _priceBe;
    private int _priceRp;
    private string _quote;
    private bool _isMelee;

    private Specie _selectedSpecie;
    private List<Specie> _species;
    private Region _selectedRegion;
    private List<Region> _regions;
    private Difficulty _selectedDifficulty;
    private List<Difficulty> _difficulties;

    private List<Role> _roles;
    private List<Tag> _tags;

    public ChampionFormViewModel(int championId = 0)
    {
        if (championId != 0)
        {
            FetchChampionAndRelations();
        }
        FetchRequiredModels();
    }

    private async void FetchRequiredModels()
    {
        Species = await new SpecieRepository().FindAll();
        Regions = await new RegionRepository().FindAll();
        Difficulties = await new DifficultyRepository().FindAll();
    }

    private void FetchChampionAndRelations()
    {
        
    }
    
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            NotifyPropertyChanged(vm => vm.Name);
        }
    }
    
    public string Alias
    {
        get => _alias;
        set
        {
            _alias = value;
            NotifyPropertyChanged(vm => vm.Alias);
        }
    }

    public DateTime ReleaseDate
    {
        get => _releaseDate;
        set
        {
            _releaseDate = value;
            NotifyPropertyChanged(vm => vm.ReleaseDate);
        } 
    }

    public int PriceBe
    {
        get => _priceBe;
        set
        {
            _priceBe = value;
            NotifyPropertyChanged(vm => vm.PriceBe);
        }
    }
    
    public int PriceRp
    {
        get => _priceRp;
        set
        {
            _priceRp = value;
            NotifyPropertyChanged(vm => vm.PriceRp);
        }
    }
    
    public string Quote
    {
        get => _quote;
        set
        {
            _quote = value;
            NotifyPropertyChanged(vm => vm.Quote);
        }
    }

    public bool IsMelee
    {
        get => _isMelee;
        set
        {
            _isMelee = value;
            NotifyPropertyChanged(vm => vm.IsMelee);
        }
    }

    public Specie SelectedSpecie
    {
        get => _selectedSpecie;
        set
        {
            _selectedSpecie = value;
            NotifyPropertyChanged(vm => vm.SelectedSpecie);
        }
    }

    public List<Specie> Species
    {
        get => _species;
        set
        {
            _species = value;
            NotifyPropertyChanged(vm => vm.Species);
        }
    }
    
    public Region SelectedRegion
    {
        get => _selectedRegion;
        set
        {
            _selectedRegion = value;
            NotifyPropertyChanged(vm => vm.SelectedRegion);
        }
    }
    
    public List<Region> Regions
    {
        get => _regions;
        set
        {
            _regions = value;
            NotifyPropertyChanged(vm => vm.Regions);
        }
    }
    
    public Difficulty SelectedDifficulty
    {
        get => _selectedDifficulty;
        set
        {
            _selectedDifficulty = value;
            NotifyPropertyChanged(vm => vm.SelectedDifficulty);
        }
    }
    
    public List<Difficulty> Difficulties
    {
        get => _difficulties;
        set
        {
            _difficulties = value;
            NotifyPropertyChanged(vm => vm.Difficulties);
        }
    }

    public List<Tag> Tags
    {
        get => _tags;
        set
        {
            _tags = value;
            NotifyPropertyChanged(vm => vm.Tags);
        }
    }
    
    public List<Role> Roles
    {
        get => _roles;
        set
        {
            _roles = value;
            NotifyPropertyChanged(vm => vm.Roles);
        }
    }
}