using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class VerwaltungView : UserControl
{
    public VerwaltungViewModel VerwaltungViewModel { get; }

    public VerwaltungView(VerwaltungViewModel verwaltungViewModel)
    {
        VerwaltungViewModel = verwaltungViewModel;
        InitializeComponent();
        DataContext = VerwaltungViewModel;
        VerwaltungViewModel.InitBaseViewModelDelegateAndEvents();
    }
    
    private void VerwaltungView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!VerwaltungViewModel.IsViewModelLoaded)
        {
            ViewManager.ShowUnderPageOn<MitgliederView>(AnimatedContentControl);
            BtnMitglieder.IsChecked = true;
            VerwaltungViewModel.IsViewModelLoaded = true;
        }
    }
    
    private void OpenUnderPage(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton objButton)
        {
            switch (objButton.Name)
            {
                case "BtnMitglieder":
                    ViewManager.ShowUnderPageOn<MitgliederView>(AnimatedContentControl);
                    break;
                case "BtnMeisterschaften":
                    ViewManager.ShowUnderPageOn<MeisterschaftenView>(AnimatedContentControl);
                    break;
            }
        }
    }
}