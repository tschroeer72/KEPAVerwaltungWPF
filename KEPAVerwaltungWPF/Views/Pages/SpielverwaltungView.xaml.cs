using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class SpielverwaltungView : UserControl
{
    public SpielverwaltungViewModel SpielverwaltungViewModel { get; }

    public SpielverwaltungView(SpielverwaltungViewModel spielverwaltungViewModel)
    {
        SpielverwaltungViewModel = spielverwaltungViewModel;
        InitializeComponent();
        DataContext = SpielverwaltungViewModel;
        SpielverwaltungViewModel.InitBaseViewModelDelegateAndEvents();
    }
    
    private void OpenUnderPage(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton objButton)
        {
            switch (objButton.Name)
            {
                case "BtnEingabe":
                    ViewManager.ShowUnderPageOn<ErgebniseingabeView>(AnimatedContentControl);
                    break;
                case "BtnAusgabe":
                    ViewManager.ShowUnderPageOn<ErgebnisausgabeView>(AnimatedContentControl);
                    break;
            }
        }
    }

    private void SpielverwaltungView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!SpielverwaltungViewModel.IsViewModelLoaded)
        {
            ViewManager.ShowUnderPageOn<ErgebniseingabeView>(AnimatedContentControl);
            BtnEingabe.IsChecked = true;
            SpielverwaltungViewModel.IsViewModelLoaded = true;
        }
    }
}