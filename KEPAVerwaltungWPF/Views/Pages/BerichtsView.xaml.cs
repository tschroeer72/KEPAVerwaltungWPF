using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class BerichtsView : UserControl
{
    public BerichtsViewModel BerichtsViewModel { get; }

    public BerichtsView(BerichtsViewModel berichtsViewModel)
    {
        BerichtsViewModel = berichtsViewModel;
        InitializeComponent();
        DataContext = BerichtsViewModel;
        BerichtsViewModel.InitBaseViewModelDelegateAndEvents();
    }
}