using System.Windows.Input;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels
{
    public class MenuViewModel : ViewModelBase<MenuViewModel>
    {
        public ICommand ManageChampionsCommand => new DelegateCommand(() => _windowListeners["ManageChampionsCommand"]());

        private readonly Dictionary<string, Action> _windowListeners;
        
        public MenuViewModel(Dictionary<string, Action> listeners)
        {
            _windowListeners = listeners;
        }
    }
}