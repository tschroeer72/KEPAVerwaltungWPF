using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.Enums;
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
                    ViewManager.ShowPageOnMainView<VerwaltungView>();
                    break;
                case "BtnSpielVerwaltung":
                    ViewManager.ShowPageOnMainView<SpielverwaltungView>();
                    break;
                case "BtnStatistik":
                    ViewManager.ShowPageOnMainView<StatistikView>();
                    break;
                case "BtnEMail":
                    ViewManager.ShowPageOnMainView<EMailView>();
                    break;
                case "BtnBerichte":
                    ViewManager.ShowPageOnMainView<BerichtsView>();
                    break;
                case "BtnEinstellungen":
                    ViewManager.ShowPageOnMainView<EinstellungenView>();
                    break;
            }
        }
        else if (sender is MenuItem objMenuItem)
        {
            switch (objMenuItem.Name)
            {
                case "MnuBtnLightTheme":
                    ChangeTheme(DarkLight.Light);
                    break;
                case "MnuBtnDarkTheme":
                    ChangeTheme(DarkLight.Dark);
                    break;
            }
        }
    }
    
    private void ChangeTheme(DarkLight darkLight)
    {
        //Resources.MergedDictionaries.Remove(Resources.MergedDictionaries.Last());
        //Resources.MergedDictionaries.Add(theme);

        int intLastIndex = Application.Current.Resources.MergedDictionaries.Count - 1;
        switch (darkLight)
        {
            case DarkLight.Light:
                Application.Current.Resources.MergedDictionaries[intLastIndex].Source = new Uri("pack://application:,,,/KEPAVerwaltungWPF;component/ThemeAndStyle/LightTheme.xaml", UriKind.RelativeOrAbsolute);
                break;
            case DarkLight.Dark:
                Application.Current.Resources.MergedDictionaries[intLastIndex].Source = new Uri("pack://application:,,,/KEPAVerwaltungWPF;component/ThemeAndStyle/DarkTheme.xaml", UriKind.RelativeOrAbsolute);
                break;
        }
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