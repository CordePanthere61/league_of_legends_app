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
    
    public StoryListingViewModel(IWindowAdapter adapter)
    {
        _windowAdapter = adapter;
        _storyRepository = new StoryRepository();
        FetchModels();
    }

    private async void FetchModels()
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
}