using System.Windows.Input;
using league_of_legends_app.CORE.Interfaces;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels
{
    public class MenuViewModel : ViewModelBase<MenuViewModel>
    {
        public ICommand ManageChampionsCommand => new DelegateCommand(() => _windowAdapter.Commands["ManageChampionsCommand"]());

        private readonly IWindowAdapter _windowAdapter;

        public MenuViewModel(IWindowAdapter adapter)
        {
            _windowAdapter = adapter;
        }
    }
}