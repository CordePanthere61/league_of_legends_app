using System.Windows.Controls;
using System.Windows.Input;
using league_of_legends_app.CORE.Interfaces;
using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;
using league_of_legends_app.CORE.Validators;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels;

public class ChampionFormViewModel : ViewModelBase<ChampionFormViewModel>
{
    // Inputs
    private string _name;
    private string _alias;
    private DateTime _releaseDate;
    private int _priceBe;
    private int _priceRp;
    private string _quote;
    private bool _isMelee;

    // ComboBoxes
    private Specie _selectedSpecie;
    private List<Specie> _species;
    private Region _selectedRegion;
    private List<Region> _regions;
    private Difficulty _selectedDifficulty;
    private List<Difficulty> _difficulties;

    // ListBoxes
    private List<Role> _roles;
    private List<Role> _selectedRoles;
    private List<Tag> _tags;
    private List<Tag> _selectedTags;

    private IWindowAdapter _windowAdapter;
    private ChampionRepository _championRepository;
    private int _championId = 0;

    public ChampionFormViewModel(IWindowAdapter windowAdapter,int championId = 0)
    {
        _windowAdapter = windowAdapter;
        _championRepository = new ChampionRepository();
        _championId = championId;
        if (IsEdit)
        {
            FetchChampionAndRelations();
        }
        FetchRequiredModels();
    }

    private bool IsEdit
    {
        get => _championId != 0;
    }

    private async void FetchRequiredModels()
    {
        Species = await new SpecieRepository().FindAll();
        Regions = await new RegionRepository().FindAll();
        Difficulties = await new DifficultyRepository().FindAll();
        Roles = await new RoleRepository().FindAll();
        Tags = await new TagRepository().FindAll();
        SelectedTags = new List<Tag>();
        SelectedRoles = new List<Role>();
    }

    private void FetchChampionAndRelations()
    {
        
    }
   
    #region Properties
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

    public List<Role> Roles
    {
        get => _roles;
        set
        {
            _roles = value;
            NotifyPropertyChanged(vm => vm.Roles);
        }
    }

    public List<Role> SelectedRoles
    {
        get => _selectedRoles;
        set
        {
            _selectedRoles = value;
            NotifyPropertyChanged(vm => vm.SelectedRoles);
        }
    }

    public ICommand RolesChangedCommand => new DelegateCommand<SelectionChangedEventArgs>((sender) =>
    {
        if (sender.AddedItems.Count > 0)
        {
            foreach (Role role in sender.AddedItems)
            {
                SelectedRoles.Add(role);
            }
        }
        else if (sender.RemovedItems.Count > 0)
        {
            foreach (Role role in sender.RemovedItems)
            {
                SelectedRoles.Remove(role);
            }
        }
    });

    public List<Tag> Tags
    {
        get => _tags;
        set
        {
            _tags = value;
            NotifyPropertyChanged(vm => vm.Tags);
        }
    }

    public List<Tag> SelectedTags
    {
        get => _selectedTags;
        set
        {
            _selectedTags = value;
            NotifyPropertyChanged(vm => vm.SelectedTags);
        }
    }

    public ICommand TagsChangedCommand => new DelegateCommand<SelectionChangedEventArgs>((sender) =>
    {
        if (sender.AddedItems.Count > 0)
        {
            foreach (Tag tag in sender.AddedItems)
            {
                SelectedTags.Add(tag);
            }
        }
        else if (sender.RemovedItems.Count > 0)
        {
            foreach (Tag tag in sender.RemovedItems)
            {
                SelectedTags.Remove(tag);
            }
        }
    });

    public string ConfirmButtonLabel
    {
        get => IsEdit ? "Modifier" : "Ajouter";
    }

    public ICommand ConfirmButtonCommand => new DelegateCommand(ValidateChampion);

    public ICommand CancelCommand => new DelegateCommand(_windowAdapter.Commands["CancelCommand"]);

    #endregion
    
    private void ValidateChampion()
    {
        Champion champion = new Champion
        {
            Name = Name,
            Alias = Alias,
            ReleaseDate = ReleaseDate,
            IsMelee = IsMelee,
            Quote = Quote,
            Specie = SelectedSpecie,
            Difficulty = SelectedDifficulty,
            Region = SelectedRegion
        };
        ChampionValidator validator = new ChampionValidator();
        if (!validator.Validate(champion))
        {
            _windowAdapter.Error("Please fill out every fields.");
            return;
        }
        if (IsEdit)
        {
            UpdateChampion();
            return;
        }
        InsertChampion();
    }

    private void InsertChampion()
    {
        _windowAdapter.Success("Champion inserted successfully.");
    }

    private void UpdateChampion()
    {
        _windowAdapter.Success("Champion updated successfully.");
    }
}