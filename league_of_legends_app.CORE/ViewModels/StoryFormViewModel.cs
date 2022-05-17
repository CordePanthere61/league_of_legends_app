using System.Windows.Controls;
using System.Windows.Input;
using league_of_legends_app.CORE.Interfaces;
using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels;

public class StoryFormViewModel : ViewModelBase<StoryFormViewModel>
{
    // Inputs
    private string _name;
    private string _text;
    
    // ComboBoxes
    private List<Region> _regions;
    private Region _selectedRegion;
    private List<Author> _authors;
    private Author _selectedAuthor;
    
    // ListBoxes
    private List<Champion> _champions;
    private List<Champion> _selectedChampions;
    
    private int _storyId; 
    private IWindowAdapter _windowAdapter;
    private TaskCompletionSource _areModelsLoaded;
    private RegionRepository _regionRepository;
    private AuthorRepository _authorRepository;
    private ChampionRepository _championRepository;
    private StoryRepository _storyRepository;

    public StoryFormViewModel(IWindowAdapter windowAdapter, int storyId = 0)
    {
        _windowAdapter = windowAdapter;
        _regionRepository = new RegionRepository();
        _authorRepository = new AuthorRepository();
        _championRepository = new ChampionRepository();
        _storyRepository = new StoryRepository();
        _storyId = storyId;
        FetchRequiredModels();
        if (IsEdit)
        {
            FetchSelectedStoryAndRelations();
        }
    }

    public async void FetchRequiredModels()
    {
        _areModelsLoaded = new TaskCompletionSource();
        Regions = await _regionRepository.FindAll();
        Authors = await _authorRepository.FindAll();
        Champions = await _championRepository.FindAll();
        SelectedChampions = new List<Champion>();
        _areModelsLoaded.SetResult();
    }

    private async void FetchSelectedStoryAndRelations()
    {
        var story = await _storyRepository.Find(_storyId);
        await _areModelsLoaded.Task;
        Name = story.Name;
        Text = story.Text;
        SelectedRegion = story.Region;
        SelectedAuthor = story.Author;
    }

    public async void UpdateSelectedChampions(ListBox box)
    {
        await _areModelsLoaded.Task;
        foreach (Champion champion in await _championRepository.FindAssociatedChampionsToStory(_storyId))
        {
            box.SelectedItems.Add(champion);
        }
    }

    private bool IsEdit
    {
        get => _storyId != 0;
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

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            NotifyPropertyChanged(vm => vm.Text);
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
    
    public Author SelectedAuthor
    {
        get => _selectedAuthor;
        set
        {
            _selectedAuthor = value;
            NotifyPropertyChanged(vm => vm.SelectedAuthor);
        }
    }

    public List<Author> Authors
    {
        get => _authors;
        set
        {
            _authors = value;
            NotifyPropertyChanged(vm => vm.Authors);
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
    
    public List<Champion> SelectedChampions
    {
        get => _selectedChampions;
        set
        {
            _selectedChampions = value;
            NotifyPropertyChanged(vm => vm.SelectedChampions);
        }
    } 
    
    public string ConfirmButtonLabel
    {
        get => IsEdit ? "Modifier" : "Ajouter";
    }

    public ICommand ConfirmButtonCommand => new DelegateCommand(ValidateStory);
    
    public ICommand CancelCommand => new DelegateCommand(_windowAdapter.Close);

    public ICommand AddNewAuthor => new DelegateCommand(_windowAdapter.Commands["AddNewAuthor"]);
    
    public ICommand SelectedChampionsChangedCommand => new DelegateCommand<SelectionChangedEventArgs>((sender) =>
    {
        if (sender.AddedItems.Count > 0)
        {
            foreach (Champion champion in sender.AddedItems)
            {
                SelectedChampions.Add(champion);
            }
        }
        else if (sender.RemovedItems.Count > 0)
        {
            foreach (Champion champion in sender.RemovedItems)
            {
                SelectedChampions.Remove(champion);
            }
        }
    });
    
    #endregion

    private void ValidateStory()
    {
        
    }
    
}