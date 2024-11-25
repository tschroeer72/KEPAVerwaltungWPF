using System.Windows.Controls;
using KEPAVerwaltungWPF.ViewModels;

namespace KEPAVerwaltungWPF.Views.Pages;

public partial class MitgliederView : UserControl
{
    public MitgliederViewModel MitgliederViewModel { get; }

    public MitgliederView(MitgliederViewModel mitgliederViewModel)
    {
        MitgliederViewModel = mitgliederViewModel;
        InitializeComponent();
        DataContext = MitgliederViewModel;
        MitgliederViewModel.InitBaseViewModelDelegateAndEvents();
    }
}