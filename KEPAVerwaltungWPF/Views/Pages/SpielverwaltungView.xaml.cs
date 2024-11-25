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
}