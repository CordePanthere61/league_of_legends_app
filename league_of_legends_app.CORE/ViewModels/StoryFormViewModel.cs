using System.Windows.Controls;
using System.Windows.Input;
using league_of_legends_app.CORE.Interfaces;
using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;
using league_of_legends_app.CORE.Validators;
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

    public async void ReloadAuthors()
    {
        Authors = await _authorRepository.FindAll(); 
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
    
    public ICommand ChampionsChangedCommand => new DelegateCommand<SelectionChangedEventArgs>((sender) =>
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
        var story = new Story();
        story.Name = Name;
        story.Author = SelectedAuthor;
        story.Region = SelectedRegion;
        story.Text = Text;
        story.SelectedChampions = SelectedChampions;
        StoryValidator validator = new StoryValidator();
        if (!validator.Validate(story))
        {
            _windowAdapter.Error("Please fill out every fields.");
            return;
        }
        if (IsEdit)
        {
            UpdateStory(story);
            return;
        }
        InsertStory(story);
    }
    
    private async void InsertStory(Story story)
    {
        int insertedId = await _storyRepository.Insert(story);
        if (insertedId != 0)
        {
            await _championRepository.InsertOrUpdateChampionStory(insertedId, SelectedChampions);
            _windowAdapter.Success("Story inserted successfully.");
            _windowAdapter.Close();
            return;
        }
        _windowAdapter.Error("An error has occured.");
    }
    
    private async void UpdateStory(Story story)
    {
        story.Id = _storyId;
        int updatedId = await _storyRepository.Update(story);
        if (updatedId != 0)
        {
            await _championRepository.InsertOrUpdateChampionStory(story.Id, SelectedChampions);
            _windowAdapter.Success("Story updated successfully.");
            _windowAdapter.Close();
            return;
        }
        _windowAdapter.Error("An error has occured.");
    }
    
    
}