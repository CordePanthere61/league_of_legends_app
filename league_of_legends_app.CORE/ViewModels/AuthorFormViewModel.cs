using System.Windows.Input;
using league_of_legends_app.CORE.Interfaces;
using league_of_legends_app.CORE.Models;
using league_of_legends_app.CORE.Repositories;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels;

public class AuthorFormViewModel : ViewModelBase<AuthorFormViewModel>
{

    private string _name;
    private IWindowAdapter _windowAdapter;
    private AuthorRepository _authorRepository;

    public AuthorFormViewModel(IWindowAdapter windowAdapter)
    {
        _windowAdapter = windowAdapter;
        _authorRepository = new AuthorRepository();
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

    public ICommand CancelCommand => new DelegateCommand(_windowAdapter.Close);
    public ICommand ConfirmCommand => new DelegateCommand(InsertAuthor);

    private async void InsertAuthor()
    {
        if (Name.Equals(null) || Name.Equals(""))
        {
            _windowAdapter.Error("Please fill out the \"Name\" field.");
            return;
        }

        var author = new Author()
        {
            Name = Name
        };
        int insertedId = await _authorRepository.Insert(author);
        if (insertedId != 0)
        {
            _windowAdapter.Success(Name + " inserted successfully");
            _windowAdapter.Close();
            return;
        }
        _windowAdapter.Error("An error has occured.");
        _windowAdapter.Close();
    }

}