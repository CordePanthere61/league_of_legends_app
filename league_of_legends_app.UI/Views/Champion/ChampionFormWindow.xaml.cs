using System.Windows;
using league_of_legends_app.CORE.ViewModels;

namespace league_of_legends_app.Views.Champion;

public partial class ChampionFormWindow : Window
{
    public ChampionFormWindow(int championId = 0)
    {
        InitializeComponent();
        var vm = new ChampionFormViewModel();
        DataContext = vm;
    }
}