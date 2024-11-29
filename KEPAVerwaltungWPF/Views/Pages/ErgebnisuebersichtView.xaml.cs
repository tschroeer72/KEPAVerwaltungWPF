using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class ErgebnisuebersichtView : UserControl
{
    public ErgebnisuebersichtViewModel ErgebnisuebersichtViewModel { get; }

    public ErgebnisuebersichtView(ErgebnisuebersichtViewModel ergebnisuebersichtViewModel)
    {
        ErgebnisuebersichtViewModel = ergebnisuebersichtViewModel;
        InitializeComponent();
        DataContext = ErgebnisuebersichtViewModel;
        ErgebnisuebersichtViewModel.InitBaseViewModelDelegateAndEvents();
    }
}