using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using KEPAVerwaltungWPF.Enums;
using KEPAVerwaltungWPF.UserControls;
using KEPAVerwaltungWPF.ViewModels;
using KEPAVerwaltungWPF.Views.Pages;
using KEPAVerwaltungWPF.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Xceed.Wpf.AvalonDock.Properties;

namespace KEPAVerwaltungWPF.Views;

public class ViewManager
{
    private static UserControl? GoBackPage { get; set; }
    public static MainWindow? MainView { get; set; }
    public static ServiceProvider? ServiceProvider { get; set; }

    public static void InitViewManager(MainWindow mainView, ServiceProvider serviceProvider)
    {
        MainView = mainView;
        ServiceProvider = serviceProvider;
    }
    
    
    public static void ShowPageOnMainView<T>(bool bFullPage = false) where T : UserControl
    {
		try
		{
            var pageService = ServiceProvider?.GetService<T>();
            if (pageService == null) return;
            if (pageService is UserControl page) 
            {
                Show(page, bFullPage);
            }
		}
		catch (Exception ex)
		{
            //MessageBox.Show(ex.ToString());
            ShowInformationWindow(ex.ToString());
        }
    }

    public static void Show(UserControl ucPage, bool bFullPage = false)
    {
        MainView!.AnimatedContentControl.ShowPage(ucPage);

        if (bFullPage) 
        {
            MainView!.TitleBar.Visibility = Visibility.Collapsed;
        }
        else
        {
            MainView!.TitleBar.Visibility=Visibility.Visible;
            GoBackPage = ucPage;
        }

        DoChangeOnViewAfterShow(ucPage);
    }

    public static void DoChangeOnViewAfterShow(UserControl ucPage)
    {
        MainView!.BtnHome.IsChecked = ucPage is HomeView;
        //MainView!.BtnVerwaltung.IsChecked = ucPage is VerwaltungView;
        //MainView!.BtnAdmin.IsChecked = ucPage is AdminView;

        MainView!.ShowCloseButton = true;
        switch (ucPage.Name)
        {
            case "HomeView":
                MainView!.BtnHome.IsChecked = true;
                MainView!.BtnVerwaltung.IsChecked = false;
                MainView!.BtnEinstellungen.IsChecked = false;
                break;
            case "VerwaltungView":
                MainView!.BtnHome.IsChecked = false;
                MainView!.BtnVerwaltung.IsChecked = true;
                MainView!.BtnEinstellungen.IsChecked = false;
                break;
            case "AdminView":
                MainView!.BtnHome.IsChecked = false;
                MainView!.BtnVerwaltung.IsChecked = false;
                MainView!.BtnEinstellungen.IsChecked = true;
                break;
        }
    }
    
    public static void GoBackOrToHome()
    {
        if(GoBackPage == null)
        {
            ShowPageOnMainView<HomeView>();
            return;
        }

        Show(GoBackPage);
    }

    public static void ShowUnderPageOn<T>(AnimatedContentControl animatedContentControl) where T : UserControl
    {
        try
        {
            var pageService = ServiceProvider?.GetService<T>();
            if (pageService == null) return;
            if (pageService is UserControl ucPage)
            {
                animatedContentControl.ShowPage(ucPage);
            }
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
            ShowInformationWindow(ex.ToString());
        }
    }

    
    private static void ShowMainInfoFlyout(string sMessage, bool bWarnung)
    {
        MainView!.LblFlyoutInfo.Content = sMessage;
        MainView!.LblFlyoutInfo.Foreground = (bWarnung) ? Brushes.DarkRed : (Brush)Application.Current.FindResource("PrimaryBrush");;
        MainView!.InfoFlyout.IsOpen = true;
    }
    public static bool ShowInformationWindow(string sMessage)
    {
        return new InfoWindow(sMessage, IWDialogType.Information).ShowDialog() ?? false;
    }

    private static bool ShowConfirmationWindow(string sMessage)
    {
        return new InfoWindow(sMessage, IWDialogType.Confirmation).ShowDialog() ?? false;
    }

    private static string ShowInputWindow(string sMessage)
    {
        var myInfoWindow = new InfoWindow(sMessage, IWDialogType.Input);
        myInfoWindow.ShowDialog();
        return myInfoWindow.InputText;
    }
    
    public static void InitBaseDelEvents(BaseViewModel baseViewModel)
    {
        baseViewModel.DelGoBackOrGotoHome += () => GoBackOrToHome();
        
        baseViewModel.DelShowInformationWindow += (msg) => ShowInformationWindow(msg);
        baseViewModel.DelShowConfirmationWindow += (msg) => ShowConfirmationWindow(msg);
        baseViewModel.DelShowInputWindow += (msg) => ShowInputWindow(msg);
        
        baseViewModel.DelShowMainInfoFlyout += (msg, warnung) => ShowMainInfoFlyout(msg, warnung);
    }
}