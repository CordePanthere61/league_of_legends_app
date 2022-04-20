using System.Windows.Input;
using SimpleMvvmToolkit.Express;

namespace league_of_legends_app.CORE.ViewModels
{
    public class MenuViewModel : ViewModelBase<MenuViewModel>
    {
        public ICommand ManageChampionsCommand => new DelegateCommand(ShowChampionsManagementWindow, CanLaunchWindow);

        public MenuViewModel()
        {
            
        }

        private void ShowChampionsManagementWindow()
        {
            
        }

        private bool CanLaunchWindow()
        {
            return true;
        }

    }
}