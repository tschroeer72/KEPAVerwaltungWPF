using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class ErgebniseingabeView : UserControl
{
    public ErgebniseingabeViewModel ErgebniseingabeViewModel { get; }

    public ErgebniseingabeView(ErgebniseingabeViewModel ergebniseingabeViewModel)
    {
        ErgebniseingabeViewModel = ergebniseingabeViewModel;
        InitializeComponent();
        DataContext = ErgebniseingabeViewModel;
        ErgebniseingabeViewModel.InitBaseViewModelDelegateAndEvents();
    }
}