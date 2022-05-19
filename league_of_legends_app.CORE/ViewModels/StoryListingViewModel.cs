using System.Windows.Input;
using league_of_legends_app.CORE.Interfaces;
using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels;

public class StoryListingViewModel : ViewModelBase<StoryListingViewModel>
{

    private IWindowAdapter _windowAdapter;
    private StoryRepository _storyRepository;

    private List<Story> _stories;
    private Story _selectedStory;
    
    public ICommand AddStoryCommand => new DelegateCommand(_windowAdapter.Commands["AddStoryCommand"]);
    public ICommand EditStoryCommand => new DelegateCommand(EditStory);

    public ICommand DeleteStoryCommand => new DelegateCommand(DeleteStory);
    
    public StoryListingViewModel(IWindowAdapter adapter)
    {
        _windowAdapter = adapter;
        _storyRepository = new StoryRepository();
        FetchModels();
    }

    public async void FetchModels()
    {
        Stories = await _storyRepository.FindAll();
    }

    public List<Story> Stories
    {
        get => _stories;
        set
        {
            _stories = value;
            NotifyPropertyChanged(vm => vm.Stories);
        }
    }

    public Story? SelectedStory
    {
        get => _selectedStory;
        set
        {
            _selectedStory = value;
            NotifyPropertyChanged(vm => vm.SelectedStory);
        }
    }
    
    private void EditStory()
    {
        if (SelectedStory == null)
        {
            _windowAdapter.Error("No story selected.");
            return;
        }
        _windowAdapter.CommandsWithId["EditStoryCommand"](SelectedStory.Id);
    }

    private void DeleteStory()
    {
        if (SelectedStory == null)
        {
            _windowAdapter.Error("No story selected.");
            return;
        }
        _windowAdapter.CommandsWithId["DeleteStoryCommand"](SelectedStory.Id);
    }
}