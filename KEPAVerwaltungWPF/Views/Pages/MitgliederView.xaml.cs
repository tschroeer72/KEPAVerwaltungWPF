using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.ViewModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Quality;
using Orientation = MigraDoc.DocumentObjectModel.Orientation;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class MitgliederView : UserControl
{
    public MitgliederViewModel MitgliederViewModel { get; }
    public PrintService PrintService { get; }

    public MitgliederView(MitgliederViewModel mitgliederViewModel, PrintService printService)
    {
        InitializeComponent();

        MitgliederViewModel = mitgliederViewModel;
        PrintService = printService;
        DataContext = MitgliederViewModel;
        MitgliederViewModel.InitBaseViewModelDelegateAndEvents();
    }

    private void BtnDrucken_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (MitgliederViewModel.StatistikSpielerErgebnisse.Count == 0) MitgliederViewModel.DruckErgebnisse = false;
            if (MitgliederViewModel.StatistikSpieler.Count == 0) MitgliederViewModel.DruckStatistik = false;

            PrintService.DruckSpielerErgebnisseStatistikAsync(MitgliederViewModel.CurrentMitglied, MitgliederViewModel.DruckErgebnisse, MitgliederViewModel.DruckStatistik);
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("MitgliederView", "BtnDrucken_OnClick", ex.ToString());
        }
    }

}