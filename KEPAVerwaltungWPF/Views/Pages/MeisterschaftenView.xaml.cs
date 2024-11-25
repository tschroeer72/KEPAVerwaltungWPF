using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class MeisterschaftenView : UserControl
{
    public MeisterschaftenViewModel MeisterschaftenViewModel { get; }

    public MeisterschaftenView(MeisterschaftenViewModel meisterschaftenViewModel)
    {
        MeisterschaftenViewModel = meisterschaftenViewModel;
        InitializeComponent();
        DataContext = MeisterschaftenViewModel;
        MeisterschaftenViewModel.InitBaseViewModelDelegateAndEvents();
    }
}