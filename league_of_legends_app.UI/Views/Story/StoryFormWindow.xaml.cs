using System;
using System.Collections.Generic;
using System.Windows;
using league_of_legends_app.CORE.ViewModels;
using league_of_legends_app.Utils;

namespace league_of_legends_app.Views.Story;

public partial class StoryFormWindow : Window
{
    private bool _firstTimeOpened = true;
    private StoryFormViewModel _viewModel;
    public StoryFormWindow(int storyId = 0)
    {
        InitializeComponent();
        var adapter = InitializeAdapter();
        if (storyId != 0)
        {
            _viewModel = new StoryFormViewModel(adapter, storyId);
            DataContext = _viewModel;
            _viewModel.UpdateSelectedChampions(SelectedChampions);
        }
        else
        {
            _viewModel = new StoryFormViewModel(adapter);
            DataContext = _viewModel;
        }
    }
    
    private void ReloadList(object sender, EventArgs e)
    {
        if (_firstTimeOpened)
        {
            _firstTimeOpened = false;
            return;
        }
        _viewModel.FetchRequiredModels();
    }

    private WindowAdapter InitializeAdapter()
    {
        var adapter = new WindowAdapter(this);
        adapter.Commands = new Dictionary<string, Action>
        {
            ["AddNewAuthor"] = AddNewAuthor
        };
        return adapter;
    }

    private void AddNewAuthor()
    {
        var window = new AuthorFormWindow();
        window.Show();
    }
}