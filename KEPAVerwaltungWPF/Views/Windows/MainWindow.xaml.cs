﻿using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KEPAVerwaltungWPF.Enums;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.ViewModels;
using KEPAVerwaltungWPF.Views.Pages;
using MahApps.Metro.Controls;
using PdfSharp.Quality;

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
                case "BtnVordrucke":
                    ViewManager.ShowPageOnMainView<VordruckeView>();
                    break;
                // case "BtnEinstellungen":
                //     ViewManager.ShowPageOnMainView<EinstellungenView>();
                //     break;
            }
        }
        else if (sender is MenuItem objMenuItem)
        {
            switch (objMenuItem.Name)
            {
                case "MnuBtnAnleitung":
                    ShowAnleitung();
                    break;
                case "MnuBtnEinstellungen":
                    ViewManager.ShowPageOnMainView<EinstellungenView>();
                    break;
                case "MnuBtnLightTheme":
                    ChangeTheme(ThemeType.Light);
                    break;
                case "MnuBtnDarkTheme":
                    ChangeTheme(ThemeType.Dark);
                    break;
                case "MnuBtnZoom":
                    EnableDisableZoom(!MainViewModel.ZoomActive);
                    break;
            }
        }
    }

    private void ShowAnleitung()
    {
        ProcessStartInfo psi = new ProcessStartInfo("Anleitung.pdf")
        {
            UseShellExecute = true
        };
        Process.Start(psi);
    }
    
    private void ChangeTheme(ThemeType themeType)
    {
        //Resources.MergedDictionaries.Remove(Resources.MergedDictionaries.Last());
        //Resources.MergedDictionaries.Add(theme);

        int intLastIndex = Application.Current.Resources.MergedDictionaries.Count - 1;
        switch (themeType)
        {
            case ThemeType.Light:
                Application.Current.Resources.MergedDictionaries[intLastIndex].Source = new Uri("pack://application:,,,/KEPAVerwaltungWPF;component/ThemeAndStyle/LightTheme.xaml", UriKind.RelativeOrAbsolute);
                Properties.Settings.Default.Theme = "Light";
                Properties.Settings.Default.Save();
                MnuBtnLightTheme.IsEnabled = false;
                MnuBtnDarkTheme.IsEnabled = true;
                break;
            case ThemeType.Dark:
                Application.Current.Resources.MergedDictionaries[intLastIndex].Source = new Uri("pack://application:,,,/KEPAVerwaltungWPF;component/ThemeAndStyle/DarkTheme.xaml", UriKind.RelativeOrAbsolute);
                Properties.Settings.Default.Theme = "Dark";
                Properties.Settings.Default.Save();
                MnuBtnLightTheme.IsEnabled = true;
                MnuBtnDarkTheme.IsEnabled = false;
                break;
        }
    }
    
    private void SwitchThemeMode()
    {
        switch (Properties.Settings.Default.Theme)
        {
            case "Light":
                ChangeTheme(ThemeType.Dark);
                MnuBtnLightTheme.IsEnabled = true;
                MnuBtnDarkTheme.IsEnabled = false;
                break;
            case "Dark":
                ChangeTheme(ThemeType.Light);
                MnuBtnLightTheme.IsEnabled = false;
                MnuBtnDarkTheme.IsEnabled = true;
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
        }
        else
        {
            //MyMagnifier.ZoomFactor = 0;
            MyMagnifier.Visibility = Visibility.Collapsed;
            MainViewModel.ZoomActive = enable;
        }

        Properties.Settings.Default.ZoomActive = enable;
        Properties.Settings.Default.ZoomFactor = MainViewModel.ZoomFactor;
        Properties.Settings.Default.ZoomRadius = MainViewModel.ZoomRadius;
        Properties.Settings.Default.Save();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        switch (Properties.Settings.Default.Theme)
        {
            case "Light":
                ChangeTheme(ThemeType.Light);
                MnuBtnLightTheme.IsEnabled = false;
                MnuBtnDarkTheme.IsEnabled = true;
                break;
            case "Dark":
                ChangeTheme(ThemeType.Dark);
                MnuBtnLightTheme.IsEnabled = true;
                MnuBtnDarkTheme.IsEnabled = false;
                break;
        }
        
        MainViewModel.ZoomActive = Properties.Settings.Default.ZoomActive;
        MainViewModel.ZoomFactor = Properties.Settings.Default.ZoomFactor;
        MainViewModel.ZoomRadius = Properties.Settings.Default.ZoomRadius;
        switch (Properties.Settings.Default.ZoomType)
        {
            case "Circle":
                MainViewModel.ZoomType = MagnifyType.Circle;
                break;
            case "Rectangle":
                MainViewModel.ZoomType = MagnifyType.Rectangle;
                break;
        }
        EnableDisableZoom(MainViewModel.ZoomActive);
        
        ViewManager.ShowPageOnMainView<HomeView>();
    }

    private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);

        // if (e.Key == Key.F12)
        // {
        //     EnableDisableZoom(!MainViewModel.ZoomActive);
        //     e.Handled = true; // Verhindert, dass andere Ereignisse ausgelöst werden
        // }

        switch (e.Key)
        {
            case Key.F1:
                ShowAnleitung();
                e.Handled = true; // Verhindert, dass andere Ereignisse ausgelöst werden
                break;
            case Key.F8:
                ViewManager.ShowPageOnMainView<EinstellungenView>();
                e.Handled = true; // Verhindert, dass andere Ereignisse ausgelöst werden
                break;
            case Key.F11:
                SwitchThemeMode();
                e.Handled = true; // Verhindert, dass andere Ereignisse ausgelöst werden
                break;
            case Key.F12:
                EnableDisableZoom(!MainViewModel.ZoomActive);
                e.Handled = true; // Verhindert, dass andere Ereignisse ausgelöst werden
                break;
        }
    }
}