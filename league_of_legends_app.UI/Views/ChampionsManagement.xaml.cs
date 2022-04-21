using System.Windows;
using league_of_legends_app.CORE.ViewModels;

namespace league_of_legends_app.Views
{
    
    public partial class ChampionsManagement : Window
    {
        public ChampionsManagement()
        {
            InitializeComponent();
            var vm = new ChampionsManagementViewModel();
            DataContext = vm;
        }
    }
}
