using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class MitgliederView : UserControl
{
    public MitgliederViewModel MitgliederViewModel { get; }

    public MitgliederView(MitgliederViewModel mitgliederViewModel)
    {
        InitializeComponent();

        MitgliederViewModel = mitgliederViewModel;
        DataContext = MitgliederViewModel;
        MitgliederViewModel.InitBaseViewModelDelegateAndEvents();
    }

    private void BtnDrucken_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (MitgliederViewModel.StatistikSpielerErgebnisse.Count == 0) MitgliederViewModel.DruckErgebnisse = false;
            if (MitgliederViewModel.StatistikSpieler.Count == 0) MitgliederViewModel.DruckStatistik = false;
            
            //Eventuell QuestPDF oder PDFSharp oder Alternative
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("MitgliederView", "BtnDrucken_OnClick", ex.ToString());
        }
    }
}