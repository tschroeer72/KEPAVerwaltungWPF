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
    
}