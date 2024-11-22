using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class HomeView : UserControl
{
    public HomeViewModel HomeViewModel { get; }

    public HomeView(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        HomeViewModel = homeViewModel;
        DataContext = HomeViewModel;
        HomeViewModel.InitBaseViewModelDelegateAndEvents();
    }
}