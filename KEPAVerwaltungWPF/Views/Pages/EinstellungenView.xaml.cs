using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.Enums;
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

    private void ZoomTypeSelection_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton rbZoomType)
        {
            switch (rbZoomType.Name)
            {
                case "RbCircle":
                    EinstellungenViewModel.ZoomTypeChanged(MagnifyType.Circle);
                    break;
                case "RbRectangle":
                    EinstellungenViewModel.ZoomTypeChanged(MagnifyType.Rectangle);
                    break;
            }
        }
    }
}