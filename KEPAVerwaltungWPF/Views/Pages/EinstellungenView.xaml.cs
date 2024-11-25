using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class EinstellungenView : UserControl
{
    public EinstellungenViewModel EinstellungenViewModel { get; }

    public EinstellungenView(EinstellungenViewModel einstellungenViewModel)
    {
        EinstellungenViewModel = einstellungenViewModel;
        InitializeComponent();
        DataContext = EinstellungenViewModel;
        EinstellungenViewModel.InitBaseViewModelDelegateAndEvents();
    }

    private void TglBtnActive_OnClick(object sender, RoutedEventArgs e)
    {
        EinstellungenViewModel.IsActive = TglBtnActive.IsChecked!.Value;
    }
}