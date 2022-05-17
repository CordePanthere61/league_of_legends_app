using System;
using System.Collections.Generic;
using System.Windows;
using league_of_legends_app.CORE.Repositories;
using league_of_legends_app.CORE.ViewModels;
using league_of_legends_app.Utils;

namespace league_of_legends_app.Views.Story;

public partial class StoryListingWindow : Window
{
    private StoryListingViewModel _viewModel;
    private StoryRepository _storyRepository;
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
        adapter.Commands = new Dictionary<string, Action>
        {
            ["AddStoryCommand"] = AddStoryCommand
        };
        adapter.CommandsWithId = new Dictionary<string, Action<int>>
        {
            ["EditStoryCommand"] = EditStoryCommand,
            ["DeleteStoryCommand"] = DeleteStoryCommand
        };
        return adapter;
    }

    public void AddStoryCommand()
    {
        var window = new StoryFormWindow();
        window.Show();
    }

    public void EditStoryCommand(int id)
    {
        var window = new StoryFormWindow(id);
        window.Show();
    }
    
    public async void DeleteStoryCommand(int id)
    {
        var story = await _storyRepository.Find(id);
        if (MessageBox.Show("Do you really want to delete the story \"" + story.Name + "\" ?", "Confirmation", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.No)
        {
            return;
        }
        await _storyRepository.Delete(await _storyRepository.Find(id));
        MessageBox.Show(story.Name + " deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}