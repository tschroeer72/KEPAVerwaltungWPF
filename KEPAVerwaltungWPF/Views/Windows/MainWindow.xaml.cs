using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
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
                case "MnuBtnZoom":
                    EnableDisableZoom(!MainViewModel.ZoomActive);
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
                Properties.Settings.Default.Theme = "Light";
                Properties.Settings.Default.Save();
                MnuBtnLightTheme.IsEnabled = false;
                MnuBtnDarkTheme.IsEnabled = true;
                break;
            case DarkLight.Dark:
                Application.Current.Resources.MergedDictionaries[intLastIndex].Source = new Uri("pack://application:,,,/KEPAVerwaltungWPF;component/ThemeAndStyle/DarkTheme.xaml", UriKind.RelativeOrAbsolute);
                Properties.Settings.Default.Theme = "Dark";
                Properties.Settings.Default.Save();
                MnuBtnLightTheme.IsEnabled = true;
                MnuBtnDarkTheme.IsEnabled = false;
                break;
        }
    }
    
    private void EnableDisableZoom(bool enable)
    {
        if (enable)
        {
            MyMagnifier.ZoomFactor = MainViewModel.ZoomFactor;
            MyMagnifier.Visibility = Visibility.Visible;
            MainViewModel.ZoomActive = enable;
            Properties.Settings.Default.ZoomActive = enable;
            Properties.Settings.Default.Save();
        }
        else
        {
            //MyMagnifier.ZoomFactor = 0;
            MyMagnifier.Visibility = Visibility.Collapsed;
            MainViewModel.ZoomActive = enable;
            Properties.Settings.Default.ZoomActive = enable;
            Properties.Settings.Default.Save();
        }
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        switch (Properties.Settings.Default.Theme)
        {
            case "Light":
                ChangeTheme(DarkLight.Light);
                MnuBtnLightTheme.IsEnabled = false;
                MnuBtnDarkTheme.IsEnabled = true;
                break;
            case "Dark":
                ChangeTheme(DarkLight.Dark);
                MnuBtnLightTheme.IsEnabled = true;
                MnuBtnDarkTheme.IsEnabled = false;
                break;
        }
        
        MainViewModel.ZoomActive = Properties.Settings.Default.ZoomActive;
        MainViewModel.ZoomFactor = Properties.Settings.Default.ZoomFactor;
        MainViewModel.ZoomRadius = Properties.Settings.Default.ZoomRadius;
        EnableDisableZoom(MainViewModel.ZoomActive);
    }

    private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);

        if (e.Key == System.Windows.Input.Key.F12)
        {
            EnableDisableZoom(!MainViewModel.ZoomActive);
            e.Handled = true; // Verhindert, dass andere Ereignisse ausgelöst werden
        }
    }
}