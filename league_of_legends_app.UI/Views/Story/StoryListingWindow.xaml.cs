using System.Windows;
using league_of_legends_app.CORE.ViewModels;
using league_of_legends_app.Utils;

namespace league_of_legends_app.Views.Story;

public partial class StoryListingWindow : Window
{
    private StoryListingViewModel _viewModel;
    public StoryListingWindow()
    {
        InitializeComponent();
        var adapter = InitializeAdapter();
        _viewModel = new StoryListingViewModel(adapter);
        DataContext = _viewModel;
    }

    private WindowAdapter InitializeAdapter()
    {
        var adapter = new WindowAdapter(this);
        //TODO commands
        return adapter;
    }
}