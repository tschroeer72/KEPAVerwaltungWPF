using System.Windows;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Windows;

public partial class SplashScreen : Window
{
    public SplashScreenViewModel SplashScreenViewModel { get; }

    public SplashScreen(SplashScreenViewModel splashScreenViewModel)
    {
        InitializeComponent();

        SplashScreenViewModel = splashScreenViewModel;
        DataContext = SplashScreenViewModel;
        SplashScreenViewModel.InitBaseViewModelDelegateAndEvents();
    }
}