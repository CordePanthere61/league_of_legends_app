using System.Windows;
using league_of_legends_app.CORE.ViewModels;

namespace league_of_legends_app.Views
{

    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            DataContext = new MenuViewModel();
        }
    }
}