using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class ErgebnisausgabeView : UserControl
{
    public ErgebnisausgabeViewModel ErgebnisausgabeViewModel { get; }

    public ErgebnisausgabeView(ErgebnisausgabeViewModel ergebnisausgabeViewModel)
    {
        ErgebnisausgabeViewModel = ergebnisausgabeViewModel;
        InitializeComponent();
        DataContext = ErgebnisausgabeViewModel;
        ErgebnisausgabeViewModel.InitBaseViewModelDelegateAndEvents();
    }
}