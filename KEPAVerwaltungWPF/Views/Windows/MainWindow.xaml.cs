using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;
using KEPAVerwaltungWPF.Views.Pages;
using MahApps.Metro.Controls;

namespace KEPAVerwaltungWPF.Views.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    public MainWindowModel MainViewModel { get; }

    public MainWindow(MainWindowModel mainViewModel)
    {
        InitializeComponent();
        MainViewModel = mainViewModel;
        DataContext = MainViewModel;
        MainViewModel.InitBaseViewModelDelegateAndEvents();
        
        //EnableDisableZoom(true);
    }
    
    private void OpenPageOnMain(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton objButton)
        {
            switch (objButton.Name)
            {
                case "BtnHome":
                    ViewManager.ShowPageOnMainView<HomeView>();
                    break;
                case "BtnVerwaltung":
                    //ViewManager.ShowPageOnMainView<VerwaltungView>(true);
                    break;
                case "BtnEinstellungen":
                    ViewManager.ShowPageOnMainView<EinstellungenView>();
                    break;
            }
        }
        // else if (sender is MenuItem objMenuItem)
        // {
        //     switch (objMenuItem.Name)
        //     {
        //         case "BtnLogout":
        //             //ViewManager.ShowLoginView(true);
        //             break;
        //     }
        // }
    }
    
    private void EnableDisableZoom(bool enable)
    {
        if (enable)
        {
            MyMagnifier.ZoomFactor = MainViewModel.ZoomFactor;
        }
        else
        {
            MyMagnifier.ZoomFactor = 0;
        }
    }
}